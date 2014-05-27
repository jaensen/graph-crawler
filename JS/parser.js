function getFrameDom(frameId) {
	
	var frame = $("#" + frameId).get(0);
	
	if (typeof frame == "undefined")
		throw new Exception("Could not find the frame element with the id " + frameId);
		
	var frameDom = frame.contentWindow.document;
	
	if (typeof frameDom == "undefined")
		throw new Exception("Could not get the frame's DOM);
		
	return frameDom;
}

/**
 * Initiates the parsing process.
 * @param {Document} frameDom The frame's DOM object.
 */
function parseFrameContent (frameDom) {
	
	// walk the whole dom and identify text nodes
	var rootElement = frameDom.documentElement;
	
	var worker = new Worker("workers/parseLinks.js");
}