Web workstation
=========
2747 The punk web - a tool for hyperactive kids

The web workstation is a tool which can be used to browse the web in an interactive and collaboratorive way. Even static html pages can be commented or annotated and shared between users.

```
License: GPL3

21.05.2014 - Verson 0.1 - Daniel Janz
             Initial version
```

Introduction
-----
The web workstation can be used as a tool to discover, describe and extend the structure of the WWW and its contents. It uses different data sources and filters to extract and connect the data and metadata of websites. This includes personal data as well because the person that maintains for example a github account belongs to the account's metadata in a way as well as the account belongs to the person's metadata.

When you're browsing the web using the web workstation you save the content of every site you visit with it permanently on your local computer and can edit it as if you've had created it. While you are browsing there can run custom filters that try to make sense of the information you're currently browsing. It can utilize differerent knowledge sources including a rdf knowledge base. Things that you miss in your knowledge base can be added and classified with little effort. 

Filters could do for example the job of ...

* ... parsing a GitHub, Twitter, LinkedIn, Facebook whatever account page
* ... parsing stock ticker, sport results, wheather-, sensor- or currency-data
* ... parsing microformats, hashtags etc.

These filters can be written by users and shared. Filter results are available for the application so that the parsed data could be stored in the local rdf knowledge base.

The prefered way of editing the crawled pages is to insert prepared or custom RDFa templates into the site. The RDFa templates are inline-.editable so that is is easy to learn the annotation techniques. The vocabulary can be a imported or custom one.  
Filters that map found data should use the same mechanisms as a manual editor. It then is free to the application whether to store the results of the filters or not. 

Every change that is made to a page can be recorded together with the user id and a timestamp so that it can be replayed from the beginning to its current state.

Motivation
-----
The web workstation aims to be a easy to use recherche and information management system. A product like this is nessecary to bring back equal oppurtunities for everybody
Like a personal excel-sheet or access-db 

Use cases
---

While talking to people about this approach on information management a lot of them could not imagine use cases so I will list a few and talk about some in more detail.  

In general I prefer to use it simply as a notepad for anything that comes along other use cases are:

* **Personal Information Management / CRM**
* Fight asynchron information
* Knowledge Base  
* Expert systems
* **Research / Education / Gamification**
* Annotate scanned bills with company, product and warranty information, payment terms, amount, etc.
* Print 2D barcodes which point to the annotations and stick them to the paper bill
* Microblogging
* Wiki
* Blog
* **Social Network**
* Inventory management
* Risk assesment
* Search engine
* Bullshit indicator
* **This documentation**
* Assembly instructions
* **Track expenses**
* etc ...

**Personal information management / CRM**  

Use the web workstation as PIM- and/or CRM-System and manage your personal and company contacts.  
Manage contacts, the companies they work for, their relatives, contacts and web-profiles etc..  

This enables you to perform queries like:

* Find all people which work in the same company and projects as contact X
* Find all people which have been married to contact X before he/she was married to contact Y

If you're a company and add the sales to your customer you could even perform queries like:

* Find all customers which bought product X and know a customer which bought product Y
* Find all customers which bought product X and mentioned it on twitter
* Find all customers which follow a person on twitter which commented negative about a product of your company
* Measure the impact of a campaign on twitter and relate the products mentioned in the tweets with your product portfolio and compare it to the product portfolio of the competitors

**Research / Education / Gamification**  

One can define a target function as filter and the quest is to find a filter pattern which matches the target function using the supplied input. This is already a description of a problem plus the description of a accepted solution. Everything inbetween must then be a way to solve the described problem.  

This mechanism can be utilized in different target applications to provide the data and the target function definition. Using this approach one could create a crowdsourcing application.

**Social Network**  

When a sufficient vocabulary is used one can create a distributed social network by creating RDFa snippets which describe a person and its contacts. Every user can theoretically use his own vocabulary and highlight the difference to existing vocabularies, thisway he can describe the relations to people and objets in a more precise way than this would have been possible with prebuilt tmeplates.

A common vocabulary for this task is the FOAF vocabulary.

**This documentation**


**Track expenses**
You can use the web workstation to track all your expenses and relate it to activities, persons etc..
Calcuate your own inflation rate.

Crawl pages
-----

New content can only be created in the context of a crawled site. This new content can then in turn be crawled again to produce new content in this context. For some use cases this might not be the desired behavior so there can be also a "home"-page which is automatically created for each user which can then act as root for all other pages.

Every page that is visited using the web worksation will be crawled and saved to the local disk. Also the browsing history is saved as a path in the graph (which can have branches for tabs) so that the additional information that is collected while browsing can be reviewd in context later.
The crawling consists out of a few different parts. Proposed built-in filters which should be executed whenever crawling a page are:

**References filter**

The minimal implementation downloads the wohle document and optionally all refrenced resources. It also logs some entires about what it does into the rdf database:

* The crawled resource with its uri as label and the local storage location as well as creation date, time and user as property
* The URIs of all refrerenced resources in this document (other documents, images, audio, video etc..)

**Words filter**

The full implementation also covers the words which are used in the page.  
Pages are processed word by word so that the parser knows their position within the text. 

1. First word:
 * Word unknown: Create new word-node and link to its origin page or page + xpath to html-element.  
 * Word known: Create a new word node and link it to the origin and the other already found mentions of that word
2. Create a "first-word" marker link between the word and the parsed page
3. Get the next word and process it just like 1.
4. Create a "next-word" marker link from the previously processed word to the current
5. * if last word: Create a "last-word" marker link between the word and the parsed page and *return*
   * else: *Continue with 3.*

When the words are linked to their position in the document e.g. via a XPath expression then they can also be grouped by structural properties of documents.

This enables the detection of phrases instead of words only.

**Custom filters**

The results of custom filters can also be saved to the rdf database and therefore these can and should be part of the crawling process too. Custom filters are described later in this doocument.
  
Annotate pages
-----
  
The crawled website will be displayed in a iframe in edit mode which allows making persistent changes to the crawled page.
All changes are only linked into the document and are stored as seperate "snippets" which are accessible via a unique generated uri.
  
Instead of simply editing the text of the webpapge, the user inserts RDFa templates which he then fills out. If the user doesn't use a specific template and simply edits text, the editor will turn the edited text into a diff template which contains details about what was changed and stores this as snippet.

A template always contains additional metadata about the author and the date and time and maybe the location of the document creation.

Access user annotations
-----

The uri schema to access a snippet which was created in the context of a crawled wikipedia page about "Markdown" could be as following:  
`[htto://data.my-domain.org/][de.wikipedia.org/wiki/Markdown][/snippets/][640A3605-C2EB-46A7-B1DA-EEAD49A29CED.html]  `
  
where  

* **http://data.my-domain.org/** is the url to a specific data endpoint  
* **de.wikipedia.org/wiki/Markdown/** is the crawled page which was annotated by the user
* **snippets/** is the virtual directory for all annotations on that page
* **640A3605-C2EB-46A7-B1DA-EEAD49A29CED.html** is the name of the annotation data file
  

Custom filters
-----

Filters are javascript snippets which implement a specific interface. They can also be published as page by a user so that they can be shared between users.

They are close to the Map/Reduce algorithm and are divided into structural and morphing filters. A structural filter provides context, while a morphing filter interprets the data in the context and changes it correspondingly.

Filters should be executed in background threads using WebWorkers. Each filter result will be accessible by a unique id when it becomes available. Alternatively the *FilterPipline.create* method could include a future to the result in its return value.

**Structural filters**  

A structural filter tries to give a crawled page a meaning by utilizing the structure of the crawled page. They are the basic filter building blocks that supply the data to every other following filter.

? Can structural filters can also be applied to the RDF knowlege base ? -> in principal they could. and later should!

* Structural filters are always designed to yield a static reference system for following filters.
* Structural filters can often be generated by users using WYSIWYG "point and click" on the desired element but then may not work in every case. 


Examples:  

* XPathFilter: filters elements using a XPath expression
* RegexFilter: filters elements using a RegEx expression

```
// Example of XPathFilter usage
var stackoverflow_profile_structure = {  
    username : function(data) {
        return XPathFilter.Create(data, "h1[id='user-displayname']/text()");
    },
}
```

All filters can be named by wrapping them in a object property. So, for example if you want to parse a StackOverflow profile page you could provide the following filter to get the username, reputation and the website of a user:  

```
// to parse a few fields from a stackoverflow profile
var stackoverflow_profile_structure = {  
    username : function(data) {
        return XPathFilter.Create(data, "h1[id='user-displayname']/text()");
    },
    reputation : function(data) {
        return XPathFilter.Create(data, "/html/body/div[5]/div[2]/div/div[2]/div/div/div/div/span/a/text()");
    },
    website : function(data) {
        return XPathFilter.Create(data, "h1[id='/html/body/div[5]/div[2]/div/div[2]/div/div/div[2]/table/tbody/tr/td[2]/a/text()");
    },
};

// to get all links on a page:
var links = {
    all: function(data) {
        return XPathFilter.Create(data, "//a");
    }
};

    
```



```
var stackoverflow_profile_structure_pipeline = FilterPipeline.create(stackoverflow_profile); 

stackoverflow_profile_structure_pipeline.resultReady(function(resultId) {
    // gets the result of the finished filter pipeline
    var resultObj = filterPipeline.getResult(resultId);
    
    if (resultId == stackoverflow_profile_pipeline.id) {
    
        // this is the result of the stackoverflow_profile_pipeline
        var username = resultObj.username;
        var reputation = resultObj.username;
        var website = resultObj.username;
    }
});

stackoverflow_profile_structure_pipeline.execute(input);

```

The result objects can be saved as json serialized string and published as document or can be used inside the editor by RDFa templates.

Note that the above example filters all return text because they use the XPath /text() function. Filters could also be used to return any kind of object like a XmlElement or custom ones defined by the used filters.

*Default structural filters:*

* XPathFilter (HTML filters like a, img, h1 etc...)
* RegExFilter (-> Word filter, Numbers filter, Url filter etc...)


**Mapping filters**  
A mapping filters takes the result of a structural filter as input and outputs a value which may be from a database query using the input as filter. They also can be used to lookup words in a knowledge database and enrich every viewed document with already known semantic data.

```
var stackoverflow_profile_map = {  
    username : function(data) {
        // Query a user-like object in the knowledge base with this username
        return LookupUser.create(function() {return data().username});
    },
    reputation : function (data) {
        // simply pass the value to the next filter or frontend
        return data;
    },
    website : function(data) {
        // Query a website in the knowledge base with the uri of the crawled website
        return LookupUri.create(function() {return data().website});
    },
};
```

Their results should be included in a container format which supports a status field and a standardized vocabulary to indicate if the query succeeded or failed. This could be HTTP status codes and a message field as well.

The results of a mapping filter can be accessed just like the results of structural filters. 


**Filter pipeline**

The filter pipeline manages the execution of the different user filters. As filters can be chained together there must be a mechanism which allows filters to access the results of other filters.

```
// A structural filter:
var structure_username = {
    username = {
        filter: "XPathFilter:h1[id='user-displayname']/text()"
    }
};

// A mapping filter which uses the result of a structural filter
var map_username = {  
    username = {
        filter: "LookupUser:username"
    }
};

// or written as:
var map_username = {  
    username = {
        filter: "LookupUser:stackoverflow_profile_structure.username"
    }
};

// nested as following (notice how we are turning things upside down):
var map_username_pipeline = FilterPipeline.create(map_username);
var structure_username_pipeline = map_username_pipeline.create(structure_username); 

// execute with input data (e.g. html document)
structure_username_pipeline.execute(input);
```
This yields the following execution and access tree:
```
GlobalFilterPipeline/map_username
GlobalFilterPipeline/map_username/structure_username
```

This means that the map_username filter depends on results from the structure_username filter which has to be executed first.  
This also means that you can bring togehter multiple sources and apply a mapping or another structural filter on it when the loading of all sources is finished. We want to use this particular feature to dynamically load all included links to RDFa snippets in a document before it is displayed so this kind of abuses the filters as a template engine.

Events about finished filters etc. are bubbled from the source to the GlobalFilterPipeline object. Same is with the access to results of previous filters.  
That means that a filter can access all its dependencies but nothing more.
The GlobalFilterPipeline can access all filters because the idle event of the GlobalFilterPipeline only can be fired when all contained filters finished their execution and therefore all contained filters are dependencies.  

**Possible filter editors for non-programmers**

 * http://code.google.com/p/blockly/

Long term goals
-----
* Add authentication and content-encryption.
* Everything should run locally in the browser. (LevelGraph, LocalStorage?? whatever)
* Use DHTs to find the user objects
* Save copies of the user data on other nodes (encrypted if nessecary)
