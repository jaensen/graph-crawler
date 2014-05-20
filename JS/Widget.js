/**
 * Provides the basic functionalities of a reusable widget.
 */
var Widget = Class.extend({
	
	id : null,
	containerSelector:null,
	resourcesUrl: null,
	containerElement : null,
	widgetElement: null,
	renderedHtml: null,
	viewData: {},
	templateManager:null,
	templateName:null,
	
	/**
	 * Erstellt eine neue Widget-Instanz.
	 * 
	 * @param id string
	 * 	Die ID des Widget.
	 * 
	 * @param containerSelector string
	 * 	Der jQuery selektor, der zum Parent-Element führt. !Muss zur Zeit der Instanzierung vorhanden sein!
	 * 
	 * @param resourcesUrl string
	 * 	Eine URL, die z.B. zum Bild-Verzeichnis führt.
	 * 
	 * @param templteName
	 * 	Der Name des anzuwendenden Templates.
	 */
	init: function(id, containerSelector, resourcesUrl, templateName, templateManager) {
		this.id = id;
		this.containerSelector = containerSelector;
		this.resourcesUrl = resourcesUrl;
		this.templateManager = templateManager;
		this.templateName = templateName;
	},
	
	/**
	 * Fügt das Widget seinem Container-Element hinzu.
	 */
	_add: function() {
		
		this.containerElement = jQuery(this.containerSelector);
		
		if (!this.containerElement.exists())
			throw new Exception("Der containerSelector '" + this.containerSelector + "' liefert kein oder kein eindeutiges Element zurück");
		
		jQuery(this.containerElement).append(this.widgetElement);
	},
	
	/**
	 * Muss überschrieben werden und bindet eventhandler und andere Sachen an die erzeugten HTML-Elemente.
	 */
	_bind: function() {
		
	},
	
	/**
	 * Entfernt das Widget aus seinem Container-Element.
	 */
	_remove: function() {
		this.widgetElement.remove();
	},

	/**
	 * Prüft ob das Widget bereits in seinem Container-Element enthalten ist.
	 */
	_exists: function() {
		return this._getElement().exists();
	},
	
	/**
	 * Gibt das jQuery element zurück welches sich gerade im DOM befindet.
	 * @returns
	 */
	_getElement: function() {
		return jQuery('#' + this.id, this.containerElement);
	},
	
	/**
	 * Lädt die View-Daten des Widgets.
	 */
	_loadData: function() {
		this.viewData.id = this.id;
		this.viewData.widget = this;
	},

	/**
	 * Entfernt das widget, in wiefern vorhanden aus seinem Container, rendert es neu und fügt es dann wieder hinzu.
	 */
	_refresh: function() {

		this._loadData();		
		this._render();
		
		if (this._exists()) 
			this._getElement().replaceWith(this.widgetElement);
		else 
			this._add();
		
		this._bind();
	},

	/**
	 * Wendet das Template auf die Daten an und gibt das gerenderte Widget zurück.
	 * 
	 * @param forceReloadTemplate bool
	 * 	Ob das Template neu geladen werden soll.
	 */
	_render: function(forceReloadTemplate) {
		
		if(typeof forceReloadTemplate == 'undefined')
			forceReloadTemplate = false;
		
		this.renderedHtml = this.templateManager.Render(this.templateName, this.viewData, forceReloadTemplate);
		this.widgetElement = jQuery(this.renderedHtml);
	},
});