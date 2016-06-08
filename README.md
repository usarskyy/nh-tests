This repository contains test code that demonstrates some of the NHibernate features and can be used for basic perfornace profiling.

**Important note: all performance tests results are relative! You might get totally diffrent results on different computers.**

## Testing description

- Every scenario runs 10 times (i.e., 510 rows scenario actually retrieves 5100 rows in total)
- Warmup is performed before first scenario execution in order to allow SQL Server to cache data into RAM 
- Total execution time of 10 scenario executions is recorded (i.e., warm up phase is not included)
- Simple entities contain only base types like bool, DateTime, int, string
- Complex entities contain base types like bool, DateTime, int, string and references to other entities
- Only selects are performed

## Results:

### June, 2016

#### Hardware & Software:

- DELL XPS 17
- 16 GB RAM
- Intel 335 Series 2.5-Inch 240GB SATA3 Solid State Drive SSDSC2CT240A4K5
- CPU Intel Core i7-2670QM
- Windows 7 Ultimate, 64-bit, SP1
- Microsoft SQL Server Express Edition (64-bit), Version 10.50.4042.0 (runs locally)
- Redis 2.8.4 (runs on local VMware virtual machine, Ubuntu 14.04.3 LTS 64-bit; swap is disabled; 4 GB RAM; 4 cores are available)
- CLRProfiler 4.5 for memory consumption testing

#### Components used:
- FluentNHibernate: 2.0.3
- NHibernate: 4.0.4.4000
- Newtonsoft.Json: 8.0.3
- NHibernate.Caches.Redis: 2.0.0
- NHibernate.Caches.StackExchange.Redis: 0.0.2
- NHibernate.Caches.RtMemoryCache: 4.0.0.4000
- NHibernate.Caches.SysCache2: 4.0.0.4000
- StackExchange.Redis: 1.0.371

#### Important notes
- FluentNHibernate is used for mappings


#### Simple entities retrieval / Performance

|   | 510 rows without cache  | 54784 rows without cache  | 510 rows with cache  | 54784 rows with cache *(1)*  |
| ------------ | ------------ | ------------ | ------------ | ------------ |
| ADO.NET  | 00:00:03.68  | 00:00:09.54  | Not implemented  | Not implemented  |
| NH statefull  | 00:00:03.73  |  00:00:20.18 | -  |  - |
| NH statefull / RtCache  | -  |  - | 00:00:00.43  |  00:00:08.87 |
| NH statefull / SysCache  | -  |  - |  00:00:00.46  | 00:00:08.53  |
| NH statefull / Redis | -  | - |  00:00:04.55  | Took too much time  |
| NH statefull / Redis+JSON *(2)* | -  |  - |  00:00:05.44  | Took too much time  |
| NH stateless  | 00:00:03.67  |  00:00:17.94 | Cache not used  | Cache not used  |

#### Complex entities retrieval / Performance

|   | 510 rows without cache  | 54784 rows without cache  | 510 rows with cache  | 54784 rows with cache *(1)*  |
| ------------ | ------------ | ------------ | ------------ | ------------ |
| ADO.NET  | 00:00:03.62  | 00:00:10.35  | Not implemented  | Not implemented  |
| NH statefull  | 00:00:03.63  |  00:00:34.12 | -  |  - |
| NH statefull / RtCache  | -  |  - | 00:00:00.52  |  00:00:16.73 |
| NH statefull / SysCache  | -  |  - |  00:00:00.51  | 00:00:16.13  |
| NH statefull / Redis | -  | - |  00:00:07.13  | Took too much time  |
| NH statefull / Redis+JSON *(2)* | -  |  - |   | Took too much time  |
| NH stateless  | 00:00:03.95  |  **00:00:47.17** | Cache not used  | Cache not used  |

#### Simple entities retrieval / Memory consumption (in bytes)

- **Each scenario executed 5 times**
- Memory consumption is calculated based on "Heap Statistics -> Allocated bytes" value that shows total allocated memory **including memory allocated by CLR**

|   | 510 rows without cache  | 54784 rows without cache  | 510 rows with cache  | 54784 rows with cache *(1)*  |
| ------------ | ------------ | ------------ | ------------ | ------------ |
| ADO.NET  | 1 481 765  | 105 596 943  | Not implemented  | Not implemented  |
| NH statefull *(3)*  | 17 886 507  |  457 529 741 | -  |  - |
| NH statefull / RtCache  *(3)*  | -  |  - | 19 026 015  |  575 219 295 |
| NH statefull / SysCache *(3)*   | -  |  - |  19 117 331  | 587 477 658  |
| NH statefull / Redis *(3)*  | -  | - |  94 010 583  | Took too much time  |
| NH statefull / Redis+JSON *(2)* *(3)*  | -  |  - |  44 870 931 | Took too much time  |
| NH stateless *(3)*   | 17 129 099  |  377 538 871 | Cache not used  | Cache not used  |


1. Never perform large SELECTs on production system with distributed cache
1. JSON serializer was copied from the suggested source: https://gist.github.com/TheCloudlessSky/f60d47ad2ca4dea72583
1. Around 8 MB of memory is consumed by FluentNHibernate mappings because it builds HBM XML that is then used by NHibernate to build actual mappings
