# Projektstruktur

```
Projekt-Wurzel
| Projekt.sln
| [.gitattributes]
| .gitignore
| [bitbucket-pipelines.yml]
| [Directory.Build.props]
| [*.md]
+ source
    + ProjektA
        | ProjektA.csproj
        | *.cs
        | ...
    + ProjektB
        | ProjektB.csproj
        | *.cs
        | ...
+ tests
    + ProjektA.Tests
        | ProjektA.Tests.csproj
        | *.cs (lt. Test-Schema)
        | ...
    + ProjektB.Tests
        | ProjektB.Tests.csproj
        | *.cs (lt. Test-Schema)
        | ...
+ [build]
    | *.targets
    | *.props
+ [doc]
    | *.md
+ [cron]
    | *.xml
    | crontab
+ [samples]
    | *
```

* `[ ]` sind optional  
* in der Solution kann per Solution-Folder eine weiter Gruppierung erfolgen
