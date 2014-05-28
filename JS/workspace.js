/**
 * The root class for the workspace.
 */
var workspace = Class.extend({

	tabBar : null,

	/**
	 * Contains the url input control from the main toolbar.
	 */
	addressInput : null,
	
	/**
	 * Creates a new instance of this class.
	 */
	init: function () {
		this.wire_controlVariables();
		this.wire_controlBar();
		this.wire_tabBar();
	},
	
	/**
	 * Caches frequently used controls in member variables.
	 */
	wire_controlVariables : function() {
		this.addressInput = jQuery("#navigationAddress");
	},
	
	/**
	 * Wires all events concerning the main toolbar.
	 */
	wire_controlBar : function() {
		
		jQuery("#navigateBack").click(this.navigateBack);
		jQuery("#navigateForward").click(this.navigateForward);
		jQuery("#navigationReload").click(this.navigationReload);
		jQuery("#navigationCancel").click(this.navigationCancel);
		jQuery("#navigationNavigate").click(this.navigationNavigate);
		
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
	wire_tabBar : function() {
		this.tabBar = new workspaceTabBar();
		var _this = this;
		
		jQuery("#newTab").click(function(event) {
			_this.tabBar.newTab();
		});
	},

	/**
	 * Navigates back.
	 */
	navigateBack : function() {
		
	},
	
	/**
	 * Navigates forward.
	 */
	navigateForward : function() {
		
	},
	
	/**
	 * Reloads the contents of the active tab.
	 */
	navigationReload : function() {
		
	},
	
	/**
	 * Cancels the loading of the active tab.
	 */
	navigationCancel : function() {
		
	},
	
	/**
	 * Navigates to the URI which was entered in the this.addressInput control.
	 */
	navigationNavigate : function() {
		alert(this.addressInput.val());
	},
});