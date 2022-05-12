# DB-RedisAssignment


# Redis data format

Source Id is either `AverageWage` or `TotalNoninstructionalEmployees` depending on which dataset are requested

## Source
```
source:<id>
    measures = measure:<id>
    annotations = annotations:<id>
    name
```

## Annotations
```
annotations:<SourceName>
    SourceName
    SourceDescription
    DatasetName
    DatasetLink
    Subtopic
    TableId
    Topic
    HiddenMeasures
```

## Measure set
```
measures:<id>
```

## Measure data for "Total Noninstructional Employees"
```
datum:TotalNoninstructionalEmployees:<id>
    IDIPEDSOccupationParent
    IPEDSOccupationParent
    IDIPEDSOccupation
    IPEDSOccupation
    IDYear
    Year
    IDCarnegieParent
    CarnegieParent
    IDCarnegie
    Carnegie
    IDUniversity
    University
    TotalNoninstructionalEmployees
    SlugUniversity
```

## Measure data for "Average Wage, Average Wage Appx MOE"
```
datum:AverageWage:<id>
    IDDetailedOccupation
    DetailedOccupation
    IDYear
    Year
    AverageWage
    AverageWageAppxMOE
    SlugDetailedOccupation
```