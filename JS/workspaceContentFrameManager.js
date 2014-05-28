/**
 * Manages all content frames.
 */
var workspaceContentFrameManager = Class.extend({
	
	/**
	 * Contains a reference to this object.
	 */
	_this : null,
	
	/**
	 * Contains a reference to the workspace.
	 */
	workspace : null,
	
	
	templateManager : null,
	
	/**
	 * Creates a new instance of the workspaceContentFrameManager.
	 */
	init : function(workspace) {
		if (workspace == null || typeof workspace == "undefined")
			throw "The workspace parameter is not allowed to be null or undefined.";
		
		this.workspace = workspace;
		
		if (this.workspace.templateManager == null || typeof this.workspace.templateManager == "undefined")
			throw "The workspace's templateManager is null or undefined!";
		
		this.templateManager = this.workspace.templateManager;
		
		this._this = this;
	}
});