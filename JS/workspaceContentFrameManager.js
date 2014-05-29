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
	
	/**
	 * Contains a reference to the template manager.
	 */
	templateManager : null,
	
	/**
	 * Contains a reference to the currently active content frame (if any)
	 */
	activeContentFrame : null,
	
	contentFrameContainerId : "",
	
	contentFrameTemplateId : "_contentArea",
	
	/**
	 * Creates a new instance of the workspaceContentFrameManager.
	 */
	init : function(workspace) {
		
		throwNullOrUndefined(workspace, "The workspace parameter is not allowed to be null or undefined.");
		
		this.workspace = workspace;

		throwNullOrUndefined(this.workspace.templateManager, "The workspace's templateManager is null or undefined!");
		
		this.templateManager = this.workspace.templateManager;
		
		this._this = this;
	},
	
	newFrame : function () {
		
	},
	
	switchFrame : function (toFrame) {
		
	},
	
	closeFrame : function(frame) {
		
	}
});