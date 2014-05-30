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
	
	frames:[],
	
	/**
	 * Creates a new instance of the workspaceContentFrameManager.
	 */
	init : function(workspace) {
		
		throwNullOrUndefined(workspace, "The workspace parameter is not allowed to be null or undefined.");
		
		this.workspace = workspace;

		throwNullOrUndefined(this.workspace.templateManager, "The workspace's templateManager is null or undefined!");
		
		this.templateManager = this.workspace.templateManager;
	},
	
	
	

	
	/**
	 * Navigates back.
	 */
	back : function() {
		alert(this.id + " back");
	},
	
	/**
	 * Navigates forward.
	 */
	forward : function() {
		alert(this.id + " forward");
	},
	
	/**
	 * Reloads the contents of the active tab.
	 */
	reload : function() {
		alert(this.id + " reload");
	},
	
	/**
	 * Cancels the loading of the active tab.
	 */
	cancel : function() {
		alert(this.id + " cancel");
	},
	
	/**
	 * Navigates to the URI which was entered in the this.addressInput control.
	 */
	navigate : function(url) {
		var _this = this;
		jQuery.get(url
			, function(successData) {
				_this.getIFrame(_this.activeContentFrame).contents().find('html').html(successData);
			});
	},
	
	
	
	
	newFrame : function (frameId) {
		var contentFrameId = typeof frameId == "undefined" ? this.workspace.getId() : frameId;
		var newContentFrame = new workspaceContentFrame(contentFrameId);
		
		var contentFrameHtml = this.templateManager.render(this.contentFrameTemplateId, newContentFrame);
		var contentFrameElement = jQuery(contentFrameHtml);
		
		//var _this = this;

		/*
			Bind events....
		*/
		
		jQuery("#" + this.contentFrameContainerId).append(contentFrameElement);
		
		this.frames.push(newContentFrame);
		this.switchFrame(newContentFrame);
	
		return newTab;
	},
	
	/**
	 * Tries to get a frame by its id.
	 * @param frameId The id
	 * @returns The frame or null
	 */
	getFrameById : function(frameId) {
		for (var i = 0; i < this.frames.length; i++)
			if (this.frames[i].id == frameId)
				return this.frames[i];
		
		return null;
	},
	
	/**
	 * Gets the html iframe element for the given frame object.
	 * @param forFrame The frame object
	 * @return the iframe (as jquery object) or null
	 */
	getIFrame : function(forFrame) {
		return jQuery("#tab" + forFrame.id + "_Content");
	},
	
	switchFrame : function (toFrame) {

		throwNullOrUndefined(toFrame, "The toFrame parameter is not allowed to be null or undefined.");
		
		this.activeContentFrame = toFrame;
		
		for(var i = 0; i < this.frames.length; i ++) {
			
			var contentFrameElement = jQuery("#tab" + this.frames[i].id + "_Content");
			
			contentFrameElement.removeClass("visible_block");
			if (this.frames[i] != toFrame)
				contentFrameElement.addClass("hidden");
		}
		
		jQuery("#tab" + toFrame.id + "_Content").addClass("visible_block");
		
		this._fireOnContentFrameSwitched(toFrame);
	},
	
	closeFrame : function(frame) {
		
		throwNullOrUndefined(frame, "The tab parameter is not allowed to be null or undefined.");

		this.frames.remove(this.frames.indexOf(frame));
		jQuery("#tab" + frame.id + "_Content").remove();
		
		this._fireOnContentFrameClosed(frame);
	},
	
	
	/************************************************************************************
	 * Events
	 */
	
	onContentFrameSwitchedCallbacks : [],
	/**
	 * Registers a callback which will be called everytime the active ContentFrame changed.
	 * @param callback The callback which should be called when the active ContentFrame changed.
	 * 				   Parameters: activeContentFrame (The contentFrame which was activated)
	 */
	onContentFrameSwitched : function (callback) {
		throwNullOrUndefined(callback, "The callback parameter is null or undefined!");
		
		this.onContentFrameSwitchedCallbacks.push(callback);
	},
	
	_fireOnContentFrameSwitched : function(activeContentFrame) {
		
		throwNullOrUndefined(activeContentFrame, "The activeContentFrame parameter is null or undefined!");
		
		for (var i = 0; i < this.onContentFrameSwitchedCallbacks.length; i++) {
			this.onContentFrameSwitchedCallbacks[i](activeContentFrame);
		}
	},	
	
	
	onContentFrameClosedCallbacks : [],
	/**
	 * Registers a callback which will be called everytime a ContentFrame is closed.
	 * @param callback The callback which should be called when a ContentFrame wass closed.
	 * 				   Parameters: closedContentFrame (The contentFrame which was closed)
	 */
	onContentFrameClosed : function (callback) {
		throwNullOrUndefined(callback, "The callback parameter is null or undefined!");
		
		this.onContentFrameClosedCallbacks.push(callback);
	},
	
	_fireOnContentFrameClosed : function(closedContentFrame) {
		
		throwNullOrUndefined(closedContentFrame, "The closedContentFrame parameter is null or undefined!");
		
		for (var i = 0; i < this.onContentFrameClosedCallbacks.length; i++) {
			this.onContentFrameClosedCallbacks[i](closedContentFrame);
		}
	},	
});