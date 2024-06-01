"use strict";(self.webpackChunkdocs=self.webpackChunkdocs||[]).push([[8448],{3905:(e,t,n)=>{n.d(t,{Zo:()=>p,kt:()=>v});var a=n(7294);function r(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}function s(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);t&&(a=a.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,a)}return n}function i(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?s(Object(n),!0).forEach((function(t){r(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):s(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function o(e,t){if(null==e)return{};var n,a,r=function(e,t){if(null==e)return{};var n,a,r={},s=Object.keys(e);for(a=0;a<s.length;a++)n=s[a],t.indexOf(n)>=0||(r[n]=e[n]);return r}(e,t);if(Object.getOwnPropertySymbols){var s=Object.getOwnPropertySymbols(e);for(a=0;a<s.length;a++)n=s[a],t.indexOf(n)>=0||Object.prototype.propertyIsEnumerable.call(e,n)&&(r[n]=e[n])}return r}var l=a.createContext({}),c=function(e){var t=a.useContext(l),n=t;return e&&(n="function"==typeof e?e(t):i(i({},t),e)),n},p=function(e){var t=c(e.components);return a.createElement(l.Provider,{value:t},e.children)},d="mdxType",m={inlineCode:"code",wrapper:function(e){var t=e.children;return a.createElement(a.Fragment,{},t)}},u=a.forwardRef((function(e,t){var n=e.components,r=e.mdxType,s=e.originalType,l=e.parentName,p=o(e,["components","mdxType","originalType","parentName"]),d=c(n),u=r,v=d["".concat(l,".").concat(u)]||d[u]||m[u]||s;return n?a.createElement(v,i(i({ref:t},p),{},{components:n})):a.createElement(v,i({ref:t},p))}));function v(e,t){var n=arguments,r=t&&t.mdxType;if("string"==typeof e||r){var s=n.length,i=new Array(s);i[0]=u;var o={};for(var l in t)hasOwnProperty.call(t,l)&&(o[l]=t[l]);o.originalType=e,o[d]="string"==typeof e?e:r,i[1]=o;for(var c=2;c<s;c++)i[c]=n[c];return a.createElement.apply(null,i)}return a.createElement.apply(null,n)}u.displayName="MDXCreateElement"},3025:(e,t,n)=>{n.r(t),n.d(t,{assets:()=>l,contentTitle:()=>i,default:()=>m,frontMatter:()=>s,metadata:()=>o,toc:()=>c});var a=n(7462),r=(n(7294),n(3905));const s={sidebar_position:2},i="Overview",o={unversionedId:"introduction/overview",id:"introduction/overview",title:"Overview",description:"Kassets is an event-based system that encourages the use of the Pub/Sub pattern.",source:"@site/docs/1-introduction/overview.md",sourceDirName:"1-introduction",slug:"/introduction/overview",permalink:"/Kassets/introduction/overview",draft:!1,editUrl:"https://github.com/kadinche/Kassets/tree/docs/main/Documentation/docs/1-introduction/overview.md",tags:[],version:"current",sidebarPosition:2,frontMatter:{sidebar_position:2},sidebar:"tutorialSidebar",previous:{title:"Kassets",permalink:"/Kassets/"},next:{title:"Installation",permalink:"/Kassets/getting-started/installation"}},l={},c=[{value:"Command",id:"command",level:3},{value:"Event",id:"event",level:3},{value:"Variable",id:"variable",level:3},{value:"Collection",id:"collection",level:3},{value:"Transaction",id:"transaction",level:3},{value:"Base Classes",id:"base-classes",level:3},{value:"Unity Event Binder",id:"unity-event-binder",level:3},{value:"Json Extension",id:"json-extension",level:3}],p={toc:c},d="wrapper";function m(e){let{components:t,...n}=e;return(0,r.kt)(d,(0,a.Z)({},p,n,{components:t,mdxType:"MDXLayout"}),(0,r.kt)("h1",{id:"overview"},"Overview"),(0,r.kt)("p",null,"Kassets is an event-based system that encourages the use of the Pub/Sub pattern.\nAs a fundamental principle, consider the instance of the ",(0,r.kt)("a",{parentName:"p",href:"https://docs.unity3d.com/Manual/class-ScriptableObject.html"},"ScriptableObject")," and its ",(0,r.kt)("inlineCode",{parentName:"p"},"name")," property as a 'key' or 'Topic'.\nYou can then create a pair between the publisher and the subscriber using this 'key'. Here's an overview of Kassets features."),(0,r.kt)("h3",{id:"command"},(0,r.kt)("a",{parentName:"h3",href:"/Kassets/kassets-core/command"},"Command")),(0,r.kt)("p",null,"Implementation of ",(0,r.kt)("a",{parentName:"p",href:"https://gameprogrammingpatterns.com/command.html"},"Command pattern"),", utilizing ",(0,r.kt)("inlineCode",{parentName:"p"},"ScriptableObject")," as an alternative to an ",(0,r.kt)("inlineCode",{parentName:"p"},"Interface"),".\nThe ",(0,r.kt)("inlineCode",{parentName:"p"},"CommandCore")," class itself is an abstract class, so an implementation is required.\nCan be useful for one-way execution, i.e. logging."),(0,r.kt)("h3",{id:"event"},(0,r.kt)("a",{parentName:"h3",href:"/Kassets/kassets-core/gameevent"},"Event")),(0,r.kt)("p",null,"Event is something that happens within the program execution that requires specific response.\nImplemented as ",(0,r.kt)("inlineCode",{parentName:"p"},"GameEvent"),", each event must have at least one publisher and one subscriber.\n",(0,r.kt)("inlineCode",{parentName:"p"},"GameEvent")," is the essence of Kassets, from which other components are derived."),(0,r.kt)("h3",{id:"variable"},(0,r.kt)("a",{parentName:"h3",href:"/Kassets/kassets-core/variable"},"Variable")),(0,r.kt)("p",null,"Variable is data stored in a ",(0,r.kt)("inlineCode",{parentName:"p"},"ScriptableObject")," that can be manipulated.\n",(0,r.kt)("inlineCode",{parentName:"p"},"VariableCore")," in Kassets is derived from ",(0,r.kt)("inlineCode",{parentName:"p"},"GameEvent"),", which triggers a value-changed event."),(0,r.kt)("h3",{id:"collection"},(0,r.kt)("a",{parentName:"h3",href:"/Kassets/kassets-core/collection"},"Collection")),(0,r.kt)("p",null,"Collection is a data structure that can contain a number of items.\n",(0,r.kt)("inlineCode",{parentName:"p"},"CollectionCore")," is derived from ",(0,r.kt)("inlineCode",{parentName:"p"},"VariableCore"),".\n",(0,r.kt)("inlineCode",{parentName:"p"},"CollectionCore")," is a wrapper for ",(0,r.kt)("inlineCode",{parentName:"p"},"List")," and ",(0,r.kt)("inlineCode",{parentName:"p"},"Dictionary"),", with additional events that are triggered when an item is added, removed, cleared, or updated."),(0,r.kt)("h3",{id:"transaction"},(0,r.kt)("a",{parentName:"h3",href:"/Kassets/kassets-core/transaction"},"Transaction")),(0,r.kt)("p",null,"Transaction is a two-way event that involves requests and responses.\nEvery time a request is sent, registered response event will process the request and return back to the requester.\nOne response event can be registered at a time.\nUseful when you want to wait for event done."),(0,r.kt)("h3",{id:"base-classes"},(0,r.kt)("a",{parentName:"h3",href:"/Kassets/fundamentals/base-classes"},"Base Classes")),(0,r.kt)("p",null,"Kassets provides default base classes that is usable immediately. You can access them from from the ",(0,r.kt)("inlineCode",{parentName:"p"},"Create/Kassets/")," context menu or from ",(0,r.kt)("inlineCode",{parentName:"p"},"Assets/Create/Kassets/")," menu bar.\nNote that Base Classes use a different ",(0,r.kt)("inlineCode",{parentName:"p"},".asmdef"),". If you manage your own ",(0,r.kt)("inlineCode",{parentName:"p"},".asmdef")," references, you may need to add a reference to ",(0,r.kt)("inlineCode",{parentName:"p"},"Kassets.Base")," in your project."),(0,r.kt)("h3",{id:"unity-event-binder"},(0,r.kt)("a",{parentName:"h3",href:"/Kassets/kassets-core/gameevent#unity-event-binder"},"Unity Event Binder")),(0,r.kt)("p",null,"A Component that forwards events raised by a ",(0,r.kt)("inlineCode",{parentName:"p"},"GameEvent")," into ",(0,r.kt)("inlineCode",{parentName:"p"},"UnityEvent"),".\nAlso known as ",(0,r.kt)("inlineCode",{parentName:"p"},"EventListener")," in Scriptable Object Architecture's terms."),(0,r.kt)("h3",{id:"json-extension"},(0,r.kt)("a",{parentName:"h3",href:"/Kassets/utilities/json-extension"},"Json Extension")),(0,r.kt)("p",null,"An Editor tools to convert Kassets' Variables into a json string or local json file.\nYou can access them from Kassets variable's inspector window."))}m.isMDXComponent=!0}}]);