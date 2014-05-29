/**
 * The controller class for the tab bar.
 */
var workspaceTabManager = Class.extend({
	
	/**
	 * Contains references to all tabs.
	 */
	tabs : [],
	
	/**
	 * Contains a reference to the active tab.
	 */
	activeTab : null,
	
	/**
	 * Holds a reference to this object.
	 */
	_this : null,
	
	/**
	 * Contains a reference to the workspace.
	 */
	workspace : null,
	
	/**
	 * Contains a reference to the workspace's templateManager.
	 */
	templateManager : null,
	
	/**
	 * Contains the html id of the tab container (hardcoded at the moment, should be replaced by more dynamic code later)
	 */
	tabContainerId : "tabBar",
	
	/**
	 * Contains the id of the tab template.
	 */
	tabTemplateId : "_tab",
	
	/**
	 * Creates a new instance of the tab bar controller.
	 */
	init: function (workspace) {
		throwNullOrUndefined(workspace, "The workpsapce parameter is null or undefined!");
		
		this.workspace = workspace;

		throwNullOrUndefined(this.workspace.templateManager, "The workspace's templateManager is null or undefined!");
		
		this.templateManager = workspace.templateManager;
	},
	
	/************************************************************************************
	 * Methods
	 */
	
	/**
	 * Opens a new tab and sets it as the active tab.
	 */
	newTab : function() {
		var tabId = this.workspace.getId();
		var newTab = new workspaceTab(tabId);
		
		var tabHtml = this.templateManager.render(this.tabTemplateId, newTab);
		var tabElement = jQuery(tabHtml);
		
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
	
	/**
	 * Switches to another tab.
	 * @param toTab The tab.
	 */
	switchTab : function(toTab) {

		throwNullOrUndefined(toTab, "The workspaceTabBar.switchTab(toTab) method expects a valid workspaceTab-instance! Undefined or null was supplied.");
		
		this.activeTab = toTab;
		for(var i = 0; i < this.tabs.length; i ++) {
			jQuery("#tab" + this.tabs[i].id).removeClass("active");
		}
		jQuery("#tab" + toTab.id).addClass("active");
		
		this._fireOnTabSwitched(toTab);
	},
	
	/**
	 * Closes the given tab.
	 * @param tab The tab to close.
	 */
	closeTab : function(tab) {
		
		throwNullOrUndefined(tab, "The tab parameter is not allowed to be null or undefined.");

		this.tabs.remove(this.tabs.indexOf(tab), 1);
		jQuery("#tab" + tab.id).remove();
		
		this._fireTabClosed(tab);
	},
	
	
	
	
	/************************************************************************************
	 * Events
	 */
	
	onTabSwitchedCallbacks : [],
	/**
	 * Registers a callback which will be called everytime the active tab changed.
	 * @param callback The callback which should be called when the active tab changed.
	 * 				   Parameters: activeTab (The tab which was selected)
	 */
	onTabSwitched : function (callback) {
		throwNullOrUndefined(callback, "The callback parameter is null or undefined!");
		
		this.onTabSwitchedCallbacks.push(callback);
	},
	
	_fireOnTabSwitched : function(activeTab) {
		for (var i = 0; i < this.onTabSwitchedCallbacks.length; i++) {
			this.onTabSwitchedCallbacks[i](activeTab);
		}
	},	
	

	
	onTabClosedCallbacks : [],
	/**
	 * Registers a callback which will be called everytime a tab was closed.
	 * @param callback The callback which should be called when a tab was closed. 
	 * 				   Parameters: tab (The tab which was closed)
	 */
	onTabClosed: function (callback) {
		throwNullOrUndefined(callback, "The callback parameter is null or undefined!");
		
		this.onTabClosedCallbacks.push(callback);
	},
	
	_fireTabClosed: function(tab) {
		for (var i = 0; i < this.onTabClosedCallbacks.length; i++) {
			this.onTabClosedCallbacks[i](tab);
		}
	},
});