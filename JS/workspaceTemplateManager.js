var workspaceTemplateManager = Class.extend({
	/**
	 * Creates a new instance of this class.
	 */
	init: function () {
	},
	
	/**
	 * Queries the document for the template with the specified id and returns a clone of the innerHtml of the template.
	 * @param templateId The id of the template (e.g. '_tab', '_button' ...)
	 * @returns A clone of the inner html of the template definition.
	 */
	getTemplate : function(templateId) {
		throwNullOrUndefined(templateId, "The templateId parameter is not allowed to be null or undefined.");
		
		return jQuery("#_templates #" + templateId).html();
	},	

	/**
	 * Renders the supplied data using the given data.
	 * @param templateId The id of the template.
	 * @param data The data to render.
	 * @returns The rendered result.
	 */
	render : function(templateId, data) {
		var template = this.getTemplate(templateId);
		return Mustache.render(template, data);
	}
});