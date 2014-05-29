/**
 * Represents a tab which can be used in the workspace.
 */
var workspaceTab = Class.extend({

	/**
	 * contains the (numeric) id of the tab.
	 */
	id : 0,
	
	/**
	 * Contains the history of all visited urls (acts as stack).
	 */
	history : [],
	
	/**
	 * Contains the currently displayed url.
	 */
	activeUrl : "", 
	
	/**
	 * Collects all pages which are passed when navigating backwards (acts as stack). 
	 */
	future : [],
	
	/**
	 * Contains the content frame which is used to display the this.activeUrl.
	 */
	contentFrame : null,
	
	/**
	 * Contains the title of the tab.
	 */
	title : "New tab",
	
	/**
	 * Contains a reference to this object.
	 */
	_this : null,
	
	/**
	 * Creates a new tab and sets its (numeric) id.
	 * @param id The id of the new tab.
	 */
	init: function (id) {
		this.id = id;
		this._this = this;
	},
	
	setTitle : function(title) {
		throwNullOrUndefined(title, "The title is not allowed to be null or undefined");
	},
	
	/**
	 * Navigates one step back in the this.history and pushes the passed site on the this.future stack.
	 */
	navigateBack : function() {
		
	},
	
	/**
	 * Nabigates one step forward in the this.future and pushes the passed site on the this.history stack.
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
	 * @param url The url to load
	 */
	navigationNavigate : function(url) {
		
	},
});