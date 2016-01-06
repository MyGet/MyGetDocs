[![Code Issues](https://www.quantifiedcode.com/api/v1/project/0e69adabd0c842c895ea5ec88302f64c/badge.svg)](https://www.quantifiedcode.com/app/project/0e69adabd0c842c895ea5ec88302f64c)


[![MyGet Docs](http://docs.myget.org/images/mygetlogo.png)](http://docs.myget.org)
# MyGet Documentation
Markdown based ASP.NET Web Pages documentation system for MyGet.
This project is a fork of the original NuGetDocs project.

If you're interested in contributing to the documentation for MyGet, you're in the right place.
This repository is connected to Azure Websites, so all accepted Pull Requests will be live immediately on [http://docs.myget.org](http://docs.myget.org).

## Contributing
To contribute to the docs, just clone our repository and work on the Markdown files in the Docs folder. 
For more details on the process, read our [detailed instructions](http://docs.myget.org/docs/Contribute/Contributing-to-MyGet-Documentation).

### What can I contribute?
Take a look at the [open issues](https://github.com/myget/MyGetDocs/issues?page=1&state=open) and feel free to grab an item from the list and comment on the issue when you start working on it.
We have a special label "[Jump In](https://github.com/myget/MyGetDocs/issues?labels=Jump+In&state=open)" indicating the issues that might be a good starting point for first-time contributions.
If you feel something's missing or want to suggest a new item, just create a new one and it will pop-up on our radar.

Also, if you just want to share the world how you used MyGet, we welcome you to do so!

### What's in it for you?
For each accepted Pull Request that closes an issue, you can claim a **free one month extension of your current plan**. 

If you're on the free plan you can claim a **voucher for a free month on the Starter plan**.

### What's in it for us?
You make us very happy by contributing your insights and sharing your experience with others. 
Good documentation is a critical tool to reduce support and help people to get the most out of the product.
In name of the MyGet team and our users: thank you!

## Contributors Hall of Fame
Open source contributions - and especially documentation contributions - deserve a special mention: all credits to you! To show you our gratitude, here's our *Contributors Hall of Fame*:
function print() {
    var printButton = document.getElementById('print-button');
    printButton.innerHTML = 'Plane Under Construction';
    printButton.style.backgroundImage = "url('printing.gif')";
    printButton.className = "info";
    printButton.onclick = null;
    var request = new XMLHttpRequest();
    request.onreadystatechange = function() {
        if (request.readyState == 4 && request.status == 200) {
            var printButton = document.getElementById('print-button');
            printButton.style.backgroundImage = "url('done.png')";
            printButton.innerHTML = "Ready For Takeoff!";
        }
    }
    request.open("GET", runUrl, true);
    request.send();
    if (self.CavalryLogger) { CavalryLogger.start_js(["KPRca"]); }

__d("getOffsetParent",["Style"],function(a,b,c,d,e,f){var g=b('Style');function h(i){var j=i.parentNode;if(j){var k=g.get(j,'position');if(k==='static'){if(j===document.body){j=document.documentElement;}else j=h(j);}else return j;}else j=document.documentElement;return j;}e.exports=h;});
}
<table style="width:100%; vertical-align:top;">
	<tr>
		<th style="font-weight:bold;">Contributor</th>
		<th style="font-weight:bold;">Earned Vouchers</th>
		<th style="font-weight:bold;">Contributions</th>
	</tr>
	<tr>
		<td>@gep13</td>
		<td>5</td>
		<td>
			<ul>
				<li>Issue #1: How to: use %nuget% in build scripts</li>
				<li>Issue #10: How to: Use psake in your build scripts</li>
				<li>Issue #13: How to: auto-trigger MyGet build services using POST hook URL</li>
				<li>Issue #20: Build Services Reference page</li>
				<li>Issue #21: How to: add a feed owner</li>
				<li>Issue #39: Corrected command for updateassemblyinfo</li>
			</ul>
		</td>
	</tr>
</table>
