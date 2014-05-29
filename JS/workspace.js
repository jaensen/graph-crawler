/**
 * The root class for the workspace.
 */
var workspace = Class.extend({

	/**
	 * Contains a reference to the tab bar controller.
	 */
	tabManager : null,
	
	/**
	 * Contains a reference to the content frame manager.
	 */
	contentFrameManager : null,

	/**
	 * Contains the url input control from the main toolbar.
	 */
	addressInput : null,
	
	/**
	 * A global counter which is used to get unique IDs for html elements.
	 */
	globalCounter : 0,
	
	/**
	 * Contains a reference to the templateManager.
	 */
	templateManager : null,
	
	/**
	 * Creates a new instance of this class.
	 */
	init: function () {
		this._wire_controlVariables();
		this._wire_templateManager();
		this._wire_controlBar();
		this._wire_tabBar();
		this._wire_contentFrameManager();
	},
	
	/**
	 * Caches frequently used controls in member variables.
	 */
	_wire_controlVariables : function() {
		this.addressInput = jQuery("#navigationAddress");
	},
	
	/**
	 * Wires all events concerning the main toolbar.
	 */
	_wire_controlBar : function() {
		
		var _this = this;
		
		jQuery("#navigateBack").click(function(event) {
			_this.navigateBack();
		});
		jQuery("#navigateForward").click(function(event) {
			_this.navigateForward();
		});
		jQuery("#navigationReload").click(function(event) {
			_this.navigationReload();
		});
		jQuery("#navigationCancel").click(function(event) {
			_this.navigationCancel();
		});
		jQuery("#navigationNavigate").click(function(event) {
			_this.navigationNavigate();
		});
		
		var _this = this;
		this.addressInput.keyup(function(event) {
			
			if(event.keyCode != 13)
				return;
			
			_this.navigationNavigate(); 
		});
	},
	

	/************************************************************************************
	 * "Private" methods
	 */
	
	/**
	 * Wires all events concerning the tab bar.
	 */
	_wire_tabBar : function() {
		this.tabManager = new workspaceTabManager(this);
		var _this = this;
		
		jQuery("#newTab").click(function(event) {
			_this.tabManager.newTab();
		});

		this.tabManager.onNewTab(function(newTab) {
			_this._handleNewTab(newTab);
		});

		this.tabManager.onTabSwitched(function(activeTab) {
			_this._handleTabSwitched(activeTab);
		});

		this.tabManager.onTabClosed(function(closedTab) {
			_this._handleTabClosed(closedTab);
		});
	},
	
	/**
	 * Wires the content frame manager.
	 */
	_wire_contentFrameManager : function() {
		this.contentFrameManager = new workspaceContentFrameManager(this);
	},
	
	/**
	 * Wires the template manager.
	 */
	_wire_templateManager : function() {
		this.templateManager = new workspaceTemplateManager();
	},	
	
	_checkEnvironment : function () {
		throwNullOrUndefined(this.tabManager.activeTab, "No active tab!");
		throwNullOrUndefined(this.contentFrameManager.activeContentFrame, "No active content frame!");
	},
	

	/************************************************************************************
	 * "Public" methods
	 */
	
	/**
	 * Increments the gloabl id by one and returns the value.
	 */
	getId : function () {
		return this.globalCounter++;
	},

	/**
	 * Navigates back.
	 */
	navigateBack : function() {
		this._checkEnvironment();
		
		this.contentFrameManager.activeContentFrame.back();
	},
	
	/**
	 * Navigates forward.
	 */
	navigateForward : function() {
		this._checkEnvironment();
		
		this.contentFrameManager.activeContentFrame.forward();
	},
	
	/**
	 * Reloads the contents of the active tab.
	 */
	navigationReload : function() {
		this._checkEnvironment();
		
		this.contentFrameManager.activeContentFrame.reload();
	},
	
	/**
	 * Cancels the loading of the active tab.
	 */
	navigationCancel : function() {
		this._checkEnvironment();
		
		this.contentFrameManager.activeContentFrame.cancel();
	},
	
	/**
	 * Navigates to the URI which was entered in the this.addressInput control.
	 */
	navigationNavigate : function() {
		this._checkEnvironment();
		
		this.contentFrameManager.activeContentFrame.navigate();	
	},
	

	/************************************************************************************
	 * Event handlers
	 */	
	_handleNewTab : function(newTab) {
		this.contentFrameManager.newFrame(newTab.id);
	},
	
	_handleTabSwitched : function(activeTab) {
		this.contentFrameManager.switchFrame(this.contentFrameManager.getFrameById(activeTab.id));
	},
	
	_handleTabClosed : function(closedTab) {
		this.contentFrameManager.closeFrame(this.contentFrameManager.getFrameById(closedTab.id));
	},
});