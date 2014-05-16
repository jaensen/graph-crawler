**A simple crawler which searchs for the appearence of nodes from a graph in a downloaded text.**

* crawl?url={url}  
_downloads a web page and searches for words which appear in the page and in the graph. Found words are is sorted by classes and instances._  

* addNode?label={label}&type={type}  
_adds a node to the graph (Type can be everything in principal but is pratically limited to 'Class' and 'Instance' at the moment)_  

* addEdge?fromNode={fromNode}&toNode={toNode}&predicate={predicate}  
_adds a new edge to the graph._  

* findNodes?labelStartsWith={labelStartsWith}  
_finds nodes by matching the first characters of a node's label._  

* findEdges?labelStartsWith={labelStartsWith}  
_finds edges by matching the first characters of a edges's label._  

* findEdgesFromNode?fromNode={fromNode}  
_finds all edges which lead from the given node to antoher_  

* findEdgesToNode?toNode={toNode}  
_finds all edges which lead to the given node_  

* loadNodes?nodes={nodes}  
_loads all node data for all given node IDs_ 

* loadEdges?edges={edges}  
_loads all edge data for all given edge IDs_ 

* loadClientCode/apps/{app}  
_loads static files from the configured "client_code" directory_


**Prerequisites**
* Mono/.Net 4.5

**Configuration**  
All configuration is done in the _Liv.io.GraphCrawler.ControlService's App.config_ file.  
  
Parameters are:
* DataDirectory: A string which points to the directory where the graph-files and downloaded resources should be stored
* EdgesFilename: The name of the file in which to store the edges
* NodesFilename: The name of the file in which to store the nodes
* ResourcesTableFilename: The name of the file in which to store the reource-entries (metdadata for downloaded sites)
* ResourcesFolder: The name of the folder in which the downloaded resources should be stored (must lie within the DataDirectory)
* ClientCodeFolder: A folder in which static files can be stored. They can be delivered directly by the application.
  

**File format**  
The tool uses csv files for persistence. All columns are seperated by pipes '|'.

___nodes.csv___ 
Id|Label|Type

_Example row_:  
694|Canada|Instance

___edges.csv___
Id|Source|Target|Type|Label|Weight  

_Example row_:  
196|85|99|Directed|lies-in|1

___resources.csv___
Uri|Title|FilesystemLocation

_Example row_
http://de.wikipedia.org/wiki/Markdow|Markdown|/var/crawler/resources/5b7c8499-6c32-478c-abd6-cf33153d0967
