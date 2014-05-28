/**
 * The controller class for the tab bar.
 */
var workspaceTabBar = Class.extend({
	
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
	 * Creates a new instance of the tab bar controller.
	 */
	init: function () {
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
			throw new Exception ("The workspaceTabBar.switchTab(toTab) method expects a valid workspaceTab-instance! Undefined or null was supplied.");
		
		this.activeTab = toTab;
	},
	
	/**
	 * Closes the given tab.
	 * @param tab The tab to close.
	 */
	closeTab : function(tab) {
		
	}
});