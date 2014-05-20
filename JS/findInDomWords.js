// jQuery plugin, example:
(function($)
{
	$.fn.styleTextNodes = function(classes, instances)
	{
		return this.each(function()
		{
			styleTextNodes(this, classes, instances);
		});
	};
})(jQuery)

function styleTextNodes(element, classes, instances)
{
	var a = document.createElement('a');
	a.className = 'classLink';

	// Recursively walk through the children, and push text nodes in the list
	var text_nodes =
	[];
	(function recursiveWalk(node)
	{
		if (node)
		{
			node = node.firstChild;
			while (node != null)
			{
				if (node.nodeType == 3)
				{
					// Text node, do something, eg:
					text_nodes.push(node);
				} else if (node.nodeType == 1)
				{
					recursiveWalk(node);
				}
				node = node.nextSibling;
			}
		}
	})(element);

	var lookupClasses =
	{};
	var lookupInstances =
	{};

	for ( var i = 0; i < classes.length; i++)
	{
		lookupClasses[classes[i].Label] = 1;
	}
	for ( var i = 0; i < instances.length; i++)
	{
		lookupInstances[instances[i].Label] = 1;
	}

	// innerText for old IE versions.
	var textContent = 'textContent' in element ? 'textContent' : 'innerText';
	for ( var i = text_nodes.length - 1; i >= 0; i--)
	{
		var dummy = document.createDocumentFragment();
		var node = text_nodes[i];
		var tmp = a.cloneNode(true);

		var re = /\s+/;
		var newA = node[textContent].split(re);

		for ( var y = 0; y < newA.length; y++)
		{
			if (typeof lookupClasses[newA[y].trim()] != "undefined")
			{

				tmp[textContent] = node[textContent].replace(newA[y], "=%"
						+ newA[y] + "%=");

				dummy.appendChild(tmp);
				node.parentNode.replaceChild(dummy, node); // Replace
				break;
			}
			if (typeof lookupInstances[newA[y].trim()] != "undefined")
			{

				tmp[textContent] = node[textContent].replace(newA[y], "=["
						+ newA[y] + "]=");

				dummy.appendChild(tmp);
				node.parentNode.replaceChild(dummy, node); // Replace
				break;
			}
		}
	}
}