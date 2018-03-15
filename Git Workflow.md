# Git workflow für Projekte von *gfoidl*  

## Grundlegendes  
Es gibt eine Menge an verschiedenen Workflows die mit _git_ umgesetzt werden können und viele davon sind auch in der Literatur bekannt. Nachfolgend eine grobe Übersicht der Einsatzwecke der verschiedenen Workflows:  

* [gitflow](http://nvie.com/posts/a-successful-git-branching-model/)  
Findet Anwendung wenn eine Veröffentlichung per __terminisierten Releases__ umgesetzt werden soll. Dies betrifft v.a. Desktopanwendungen.  

* [github flow](http://scottchacon.com/2011/08/31/github-flow.html) / [feature branch workflow](https://www.atlassian.com/git/tutorials/comparing-workflows/feature-branch-workflow)  
Findet Anwendung wenn __continuous deployment__ angewandt und möglich ist. Dies betrifft v.a. Webanwendungen.  

* [centralized workflow](https://www.atlassian.com/git/tutorials/comparing-workflows/centralized-workflow)  
Wird nicht angewandt, da _Features_ (zumindest während der Entwicklung) in einem eigenen Zweig isoliert werden sollen. Auch sind so die Commit gruppiert und das erhöht die Leserlichkeit der Projekt-Geschichte.  

* [forking workflow](https://www.atlassian.com/git/tutorials/comparing-workflows/forking-workflow)  
Kann angewandt werden wenn eine _Neuentwicklung_ eines Projekts erfolgen soll. I.d.R. ergibt sich aber kein Anwendungsgrund, da Neuentwicklungen besser im vorhanden Projekt erfolgen sollten, damit die komplette Projekt-Geschichte an einem Ort vorhanden ist.  

Nachfolgend Spezifizierungen betreffend _feature branch workflow_ welche sich aus meiner bisheriger Erfahrung mit _git_ als vorteilhaft herauskristallisiert haben -- klarerweise hat sich die Sicht der Dinge geändert und das wird sich vermutlich wieder ändern (Evolution), daher ist hier der Stand per 31.10.2015 widergegeben.  

## Allgemeines  
* alle Projekte sind grundsätzlich nach *continuous delivery* ausgerichtet  
* *master* ist der Hauptbranch  
* Branches sollen eher kurzlebig sein, d.h. sie werden erstellt für ein Feature und nach dem Merge zu *master* wieder gelöscht  
* damit Branches nicht schal und zuweit hinter _master_ sind, soll(t)en diese regelmäßig auf master _rebased_ werden  
* Ist ein _Feature_ fertig, so wird es nach *master* gemerged -- egal ob es schon _live gehen_ soll od. nicht. Diesem Umstand kann per _feature toggle_ Rechnung getragen werden.  
Nach dem _live gehen_ kann das _feature toggle_ ausgebaut werden.
* langlebiege Branches sollten vermieden werden -- "Zwiesel sind für einen Baum nicht gut" und durch die nötigen Synchronisations-Merges wird die Historie nur unübersichtlich und schwer verständlich  
 
Wird auf eine regelmäßige Synchronisation (per `rebase`) verzichtet, so kommt es erst nach dem Merge zu *master* auf was fehlt und nicht geht. Daher soll bzw. __muss__ vor einem Merge zu *master* eine Sync. durchgeführt werden, so dass allfällige Änderungen und Anpassungen noch im Zweig (aber _on top of master_) durchgeführt werden können.  
Andernfalls müssen diese Änderungen und Anpassungen direkt in *master* durchgeführt werden und das ist tunlichst zu vermeiden.  

## Verwendete Zeichen  
`[ ]` steht für optionale Angabe  
`{ }` dient als Platzhalter  

## Namenskonvention für Branches  
`[{präfix}/][{präfix1}]branch_name_mit_unterstrich_getrennt[-{suffix}]`   
Das *präfix* ist optional und wird verwendet um Hierarchien bzw. Gruppierungen einzuführen.  

## Features  
* `branch` von *master*  
* um mit *master* aktuell zu bleiben -> `rebase`  
* `merge` zu master  

Optional:  
* Präfix `dev`  
* nach dem Merge ein `tag` mit Namen `dev/{feature}` (cf. Versionen)  
* Feature-Branches können für Sub-Features, Bug-Fixes, etc. auch als Start- und Endpunkt von neuen Branches dienen  

Beispiel: dokument-exporer-pivotviewer  

## Issues / Bugfixes  
* `branch` von *master*  
* Präfix `fix`  
* Präfix1 `Jira-Issuekey`
* Suffix `issue#{number}` haben  

Optional:  
* nach dem Merge ein `tag` mit dem selben Namen wie der Branch  

## Versionen  
* Kennzeichnung ist optional, falls Features mit einem `tag` versehen werden, sonst obligatorisch  
* Kennzeichnung durch `tag`  
* Name `v{Version}`  
* Versionierung nach [SemVer](http://semver.org/)

## Stillgelegte / nicht verfolgte Entwicklungszweige  
* branch soll gelöscht werden  
* Kennzeichnung mit `tag`  
* Name `disc/{feature}`  

## Kunden / Mandanten   
Für Kunden bzw. Mandanten gibt es keine eigenen Branches, da auch keine speziellen Entwicklungen dafür vorgesehen werden sollen. D.h. die Codebasis soll für alle Mandanten die Gleiche sein.  

Anpassungen an Mandanten erfolgen per Konfiguration in Form von config-Transformationen (mittels SlowCheetah) und durch PlugIns. Letzlich ist es Aufgabe vom __Deploy__-Mechanismus dafür zu Sorgen, dass die Anpassungen für den Mandanten korrekt "assembliert" werden.  
In Visual Studio kann dies z.B. durch _WebDeploy_ (cf. [Workflow für Mandantentrennung](https://bitbucket.org/gfoidlkassandra/www/issues/32/workflow-f-r-mandantentrennung-berlegen)) erfolgen. Für ein Beispiel siehe [WebDeploy - Mandanten](https://bitbucket.org/gfoidltests/webdeploy-mandanten).  

## Demo-Versionen  
* `branch`von *master*  
* alle Zweige die im Demo sein sollen reinmergen  
* Anpassungen für Demo durchführen  
* `tag` mit Namen `demo/{demoname}`  

Die Commits vom Demo bleiben also in der Projekt-Historie -- es hat sich schon öfter herausgestellt, dass diese Demo-Version wieder benötigt wird und außerdem ist es so auch sauber dokumentiert.

## Plug-Ins  
Für Plug-Ins müssen zwei Fälle unterschieden werden:  
a) Plug-In ist ein integraler Bestandteil der Anwendung / Projekt  
b) Plug-In wird unabhängig von der Anwendung / Projekt entwickelt  

### Plug-Ins -- Fall a  
In diesem Fall ist das Plug-In als _Feature_ vom Projekt zu betrachten und somit ein Vorgehen gem. _feature branch workflow_ und wie bisher in diesem Dokument beschrieben anzuwenden.  

### Plug-Ins -- Fall b  
Wenn das Plug-In unabhängig vom Projekt ist, so kann überlegt werden ob für das Plug-In nicht ein selbständiges Projekt erstellt werden soll. In diesem Fall sollte das (Haupt-) Projekt die Plug-In-Definition per NuGet-Paket anbieten, so dass auch eine Versionierung vorhanden ist.  

Ist ein eigenständiges Projekt für das Plug-In nicht möglich / gewünscht / whatever, so sollte die Entwicklung des Plug-Ins in einem vewaisten (orphan) Zweig erfolgen (`git checkout --orphan <NEWBRANCH>`), denn es gibt keinen vernünftigen Startpunkt für den Zweig.
