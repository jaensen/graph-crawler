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
	 * Contains a reference to this object.
	 */
	_this : null,
	
	/**
	 * Creates a new instance of the content frame class.
	 */
	init : function() {
		this._this = this;
	},

	
});