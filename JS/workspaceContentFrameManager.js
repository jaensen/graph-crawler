/**
 * Manages all content frames.
 */
var workspaceContentFrameManager = Class.extend({
	
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
	
	contentFrameContainerId : "contentArea",
	
	contentFrameTemplateId : "_contentFrame",
	
	/**
	 * Creates a new instance of the workspaceContentFrameManager.
	 */
	init : function(workspace) {
		
		throwNullOrUndefined(workspace, "The workspace parameter is not allowed to be null or undefined.");
		
		this.workspace = workspace;

		throwNullOrUndefined(this.workspace.templateManager, "The workspace's templateManager is null or undefined!");
		
		this.templateManager = this.workspace.templateManager;
	},
	
	newFrame : function () {
		var contentFrameId = this.workspace.getId();
		var newContentFrame = new workspaceContentFrame(contentFrameId);
		
		var contentFrameHtml = this.templateManager.render(this.contentFrameTemplateId, newContentFrame);
		var contentFrameElement = jQuery(contentFrameHtml);
		
		var _this = this;

		// add the close handler to the new tab 
		jQuery(".close", tabElement).click(function (){
			_this.closeTab(newTab);
		});
		
		// add the select handler to the new tab
		jQuery(".tabCaption", tabElement).click(function (){
			_this.switchTab(newTab);
		});
		
		jQuery("#" + this.tabContainerId).append(tabElement);
		
		this.tabs.push(newTab);
		this.switchTab(newTab);
	
		return newTab;
	},
	
	switchFrame : function (toFrame) {
		
	},
	
	closeFrame : function(frame) {
		
	}
});