var GraphDbApi = Class.extend({

	apiUrl:null,
	
	init: function (apiUrl) {
		this.apiUrl = apiUrl;
	},
		
	
	/**
	 * Crawls the url and matches all words used on the crawled page against the
	 * instances and classes in the graph.
	 * 
	 * @param url
	 */
	crawl: function (url, callback) {
		this._request("crawl?url=" + url, function(data) {
			/*var example = {
					"FilesystemLocation":null,
					"Message":null,
					"ReferencedClasses": ["31","10","93","20","545","731","814"],
					"ReferencedObjects":["44","414","578"],
					"Success":true,
					"Title":null,
					"Uri":"http:\/\/en.wikipedia.org\/wiki\/Social_peer-to-peer_processes"
			};*/
			if (typeof callback != "undefined")
				callback(data);
		});
	},

	streamResource: function (uri, callback, failCallback) {
		/*
		this._request("streamResource?uri=" + uri, function(data) {
			if (typeof callback != "undefined")
				callback(data);
		});
		*/

		jQuery.ajax({
			url: this.apiUrl + "streamResource",
			data: {uri:uri},
			success: function (data) {
				
				if (typeof callback == "undefined") 
					return;

				callback(data);
			},
			error: function(data) {
				if (typeof failCallback == "undefined") 
					return;
				failCallback(data);
			},
			dataType: 'text'
		});
	},
	
	/**
	 * Adds a new node to the graph.
	 * 
	 * @param label
	 * @param type
	 */
	addNode: function (label, type, callback) {
		this._request("addNode?label=" + label + "&type=" + type, function(data) {
			/*var example = {
					"Id":"0",
					"Label":"bla",
					"Message":"",
					"Success":true,
					"Type":"bla"
			};*/
			if (typeof callback != "undefined")
				callback(data);
		});
	},
	
	/**
	 * Removes a node from the graph.
	 * @param node The id of the node to remove.
	 */
	removeNode: function(node) {
		this._request("removeNode?node=" + node, function(data) {
			/*var example = {
					"Message":"",
					"Success":true,
			};*/
			if (typeof callback != "undefined")
				callback(data);
		});
	},
	
	/**
	 * Adds a new edge to the graph.
	 * 
	 * @param fromNode The id of the node from which the edge starts
	 * @param toNode The id of the node at which the edge ends.
	 * @param predicate The edge label or predicate
	 */
	addEdge: function (fromNode, toNode, predicate, callback) {
		this._request("addEdge?fromNode=" + fromNode + "&toNode=" + toNode + "&predicate=" + predicate, function(data) {
			/*
			 * {
			 * 		"Id":"19027956-163b-4957-9de9-6b3e0bfea937",
			 * 		"Label":"Hello",
			 * 		"Message":"",
			 * 		"Source":"1",
			 * 		"Success":true,
			 * 		"Target":"2",
			 * 		"Weight":0
			 * }
			 */
			if (typeof callback != "undefined")
				callback(data);
		});
	},
	
	/**
	 * Removes a edge from the graph.
	 * @param edge The id of the edge to remove.
	 */
	removeEdge: function(edge) {
		this._request("removeEdge?edge=" + edge, function(data) {
			/*
			 * {
			 * 		"Message":"",
			 * 		"Success":true,
			 * }
			 */
			if (typeof callback != "undefined")
				callback(data);
		});
	},
	
	/**
	 * Returns all nodes which label starts with the supplied string.
	 * 
	 * @param labelStartsWith
	 */
	findNodes: function (labelStartsWith, callback) {
		this._request("findNodes?labelStartsWith=" + labelStartsWith, function(data) {
			/*
			 * [
			 * 		{
			 * 			"Id":"548",
			 * 			"Label":"Dabhol Power Project ",
			 * 			"Message":"",
			 * 			"Success":true,
			 * 			"Type":"Instance"
			 * 		},{
			 * 			"Id":"827",
			 * 			"Label":"dahacouk",
			 * 			"Message":"",
			 * 			"Success":true,
			 * 			"Type":"Instance"
			 * 		}
			 * ]
			 */
			if (typeof callback != "undefined")
				callback(data);
		});
	},
	
	/**
	 * Returns all edges which label starts with the supplied string.
	 * 
	 * @param labelStartsWith
	 */
	findEdges: function (labelStartsWith, callback) {
		this._request("findEdges?labelStartsWith=" + labelStartsWith, function(data) {
			/*
			 * [
			 * 		{
			 * 			"Id":"196",
			 * 			"Label":"lies-in",
			 * 			"Message":"",
			 * 			"Source":"85",
			 * 			"Success":true,
			 * 			"Target":"99",
			 * 			"Weight":0
			 * 		},{
			 * 			"Id":"201",
			 * 			"Label":"lies-in",
			 * 			"Message":"",
			 * 			"Source":"83",
			 * 			"Success":true,
			 * 			"Target":"99",
			 * 			"Weight":0
			 * 		}
			 */
			if (typeof callback != "undefined")
				callback(data);
		});
	},
	
	/**
	 * Returns all edges which start from the supplied node.
	 * 
	 * @param fromNode
	 */
	findEdgesFromNode: function (fromNode, callback) {
		this._request("findEdgesFromNode?fromNode=" + labelStartsWith, function(data) {
			if (typeof callback != "undefined")
				callback(data);
		});
	},
	
	/**
	 * Returns all edges which are leading to the specified node.
	 * 
	 * @param toNode
	 */
	findEdgesToNode: function (toNode, callback) {
		this._request("findEdgesToNode?toNode=" + labelStartsWith, function(data) {
			if (typeof callback != "undefined")
				callback(data);
		});
	},
	
	/**
	 * Loads all information for the supplied node IDs.
	 * 
	 * @param nodes
	 */
	loadNodes: function (nodes, callback) {
		this._request("loadNodes?nodes=" + labelStartsWith, function(data) {
			if (typeof callback != "undefined")
				callback(data);
		});
	},
	
	/**
	 * Loads all information for the supplied edge IDs.
	 * 
	 * @param edges
	 */
	loadEdges: function (edges, callback) {
		this._request("loadEdges?edges=" + labelStartsWith, function(data) {
			if (typeof callback != "undefined")
				callback(data);
		});
	},
	
	/**
	 * Loads a file for a custom app.
	 * 
	 * @param app
	 */
	loadClientCode: function(fileName, callback) {
		this._request("loadClientCode/apps/" + fileName, function(data) {
			if (typeof callback != "undefined")
				callback(data);
		});
	},
	

	loadClassNodes: function(callback) {
		this._request("loadClassNodes", function(data) {
			if (typeof callback != "undefined")
				callback(data);
		});
	},
	

	loadInstanceNodes: function(callback) {
		this._request("loadInstanceNodes", function(data) {
			if (typeof callback != "undefined")
				callback(data);
		});
	},
	

	loadEdgeTypes: function(callback) {
		this._request("loadEdgeTypes", function(data) {
			if (typeof callback != "undefined")
				callback(data);
		});
	},
	
	load: function(callback) {
		this._request("load", function(data) {
			if (typeof callback != "undefined")
				callback(data);
		});
	},
	
	save: function(callback) {
		this._request("save", function(data) {
			if (typeof callback != "undefined")
				callback(data);
		});
	},
	
	_request: function(url, callback) {
		jQuery.get(
			this.apiUrl + url, 
 			function (data) {
				
				if (typeof callback == "undefined") 
					return;

				callback(data);
			});
	},
});