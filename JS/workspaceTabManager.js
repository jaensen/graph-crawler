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
		if (workspace == null || typeof workspace == "undefined")
			throw "The workpsapce parameter is null or undefined!";
		
		this.workspace = workspace;
		
		if (this.workspace.templateManager == null || typeof this.workspace.templateManager == "undefined")
			throw "The workspace's templateManager is null or undefined!";
		
		this._this = this;
	},
	
	/**
	 * Opens a new tab and sets it as the active tab.
	 */
	newTab : function() {
		var tabId = 0;
		var newTab = new workspaceTab(tabId);
		
		
		
		this.tabs.push(newTab);
		this.switchTab(newTab);
	},
	
	/**
	 * Switches to another tab.
	 * @param toTab The tab.
	 */
	switchTab : function(toTab) {
		
		if (typeof toTab == "undefined" || toTab == null)
			throw "The workspaceTabBar.switchTab(toTab) method expects a valid workspaceTab-instance! Undefined or null was supplied.";
		
		this.activeTab = toTab;
	},
	
	/**
	 * Closes the given tab.
	 * @param tab The tab to close.
	 */
	closeTab : function(tab) {
		
	}
});