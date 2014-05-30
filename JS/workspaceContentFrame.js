/**
 * Represents a frame which is used by the workspace and referenced by a tab.
 */
var workspaceContentFrame = Class.extend({
	
	/**
	 * Contains the (numeric) id of this content frame.
	 * Remarks: This id usually corresponds to the tab id.
	 */
	id : 0,
	
	/**
	 * If the iframe is in edit mode.
	 */
	editMode : false,
	
	/**
	 * Contains the history of all visited urls (acts as stack).
	 */
	history : [],
	
	/**
	 * Contains the currently displayed url.
	 */
	activeUrl : "about:blank", 
	
	/**
	 * Collects all pages which are passed when navigating backwards (acts as stack). 
	 */
	future : [],
	
	/**
	 * Creates a new instance of the content frame class.
	 */
	init : function(id) {
		this.id = id;
	},
});