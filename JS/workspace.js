/**
 * The root class for the workspace.
 */
var workspace = Class.extend({

	/**
	 * Contains a reference to the tab bar controller.
	 */
	tabBar : null,
	
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
	 * Increments the gloabl id by one and returns the value.
	 */
	_getId : function () {
		return this.globalCounter++;
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
	
	/**
	 * Wires all events concerning the tab bar.
	 */
	_wire_tabBar : function() {
		this.tabBar = new workspaceTabManager(this);
		var _this = this;
		
		jQuery("#newTab").click(function(event) {
			_this.tabBar.newTab();
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
		if (this.tabBar.activeTab == undefined || this.tabBar.activeTab == null)
			throw "No active tab!";
	},

	/**
	 * Navigates back.
	 */
	navigateBack : function() {
		this._checkEnvironment();
	},
	
	/**
	 * Navigates forward.
	 */
	navigateForward : function() {
		this._checkEnvironment();
	},
	
	/**
	 * Reloads the contents of the active tab.
	 */
	navigationReload : function() {
		this._checkEnvironment();
	},
	
	/**
	 * Cancels the loading of the active tab.
	 */
	navigationCancel : function() {
		this._checkEnvironment();
	},
	
	/**
	 * Navigates to the URI which was entered in the this.addressInput control.
	 */
	navigationNavigate : function() {
		this._checkEnvironment();
		alert(this.addressInput.val());
	},
});