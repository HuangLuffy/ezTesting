<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:output method="html" version="4.0" encoding="UTF-8" indent="yes"/>
	<xsl:param name="selected_genre" select="'all'"/>
	<xsl:param name="aaa" select="'1111'"/>
	<xsl:template match="/" name="root">
	<html>
	<head>
	<title>Transform Demo</title>
	<style type="text/css">
		body{
		background-color:#F7F7E7;
		font:12px "Arial";
		text-align:center;
		}
		.TdFailPassRateTitle{
		font:12px "Arial";
		background-color:#CEE3FF;
		Width:90px;
		font-weight: bold;
		}
		.TrTh{
		font:12px "Arial";
		background-color:orange;
		text-align:center;
		vertical-align:center;
		font-weight: bold;
		}
		.TdSummeryTotal{
		font:12px "Arial";
		background-color:orange;
		text-align:center;
		vertical-align:center;
		font-weight: bold;
		}
		.TrResult{
		text-align:center;
		vertical-align:center;
		}
		.TrResult1{
		text-align:center;
		vertical-align:center;
		}
		.SummaryResult{
		text-align:center;
		vertical-align:center;
		}
		.TdResult{
		text-align:center;
		vertical-align:center;
		}
		.ConfigLeftTable{
		width:100%;
		cellspacing:1;
		font-size:13px;
		color:black;
		}
		.ConfigRightTable{
		width:100%;
		cellspacing:1;
		font-size:13px;
		color:black;
		}
		.SummaryTable{
		width:100%;
		cellspacing:1;
		font-size:11px;
		color:black;
		}
		.ResultTable{
		width:100%;
		cellspacing:1;
		font-size:11px;
		color:black;
		}
		.NameTable{
		width:100%;
		cellspacing:1;
		font-size:18px;
		color:White;
		text-align:center;
		vertical-align:center;
		}
		.SplitTable{
		width:13%;
		cellspacing:1;
		font-size:14px;
		color:black;
		}
		.SplitButton {
		cursor:hand;
		font-size: 14px;
		color: #003300;
		background: #F7F7E7;
		border: 0px solid;
		text-align:center;
		vertical-align:center;
		width:50px;
		}
		.SplitNum {
		cursor:hand;
		font-size: 14px;
		color: #003300;
		background: #F7F7E7;
		border: 0px solid;
		text-align:center;
		vertical-align:center;
		width:30px;overflow:hidden;
		}
		.SplitNumChange {
		cursor:hand;
		font-size: 14px;
		color: #003300;
		background: #CEE3FF;
		font-weight: bold;
		border: 0px solid;
		text-align:center;
		vertical-align:center;
		width:30px;overflow:hidden;
		}
		.TrResultColor1{
		background-color:white;
		text-align:center;
		vertical-align:center;
		}
		.TrResultColor2{
		background-color:#F7F7E7;
		text-align:center;
		vertical-align:center;
		}    
		#RowTitleLayer {
		cursor:hand;
		position:relative;
		background-color:#F7F7E7;
		width:100%;
		top:-9;
		overflow-x:visible;
		overflow-y:visible;
		}
		#RowTitleTable {
		text-align:center;
		position:absolute;
		border:0;
		background-color:#F7F7E7;
		right:166;
		}
		#RowValueTableDiv {
		filter:alpha(opacity=95);
		position:absolute;
		border:0;
		background-color:#F7F7E7;
		right:0;
		Height:280;
		overflow-x:visible;
		overflow-y:auto;
		}
		#RowValueTable {
		background-color:#F7F7E7;
		}
		#RowValueTable td{
		text-align:left;
		}
		#RowTitleLayer td {
		text-align:left;
		Height:22;
		background-color:#FFEFAD;
		font-size:11px;
		width:160;
		border:1px solid Orange;
		border-bottom:1px solid Orange;
		border-right:1px solid Orange;
		}
	 </style>
	<script type="text/javascript">
						
							<![CDATA[
		var processorCache=null;
		var xmlDoc=null;
		var xslDoc=null;
		function getProcessor(transformObj){
		 var xslDoc, xslTemplate;
		 if(processorCache===null){
			if (window.ActiveXObject){
				  xslDoc=new ActiveXObject("Msxml2.FreeThreadedDOMDocument.6.0");
				  xslTemplate=new ActiveXObject("Msxml2.XSLTemplate.6.0");
				  xslDoc.async=false;
				  xslDoc.loadXML(transformObj.xml);
				  xslTemplate.stylesheet = xslDoc;
				  processorCache=xslTemplate.createProcessor();
			}
			else if (!(window.ActiveXObject) && ("ActiveXObject" in window))
			{
				  xslDoc=new ActiveXObject("Msxml2.FreeThreadedDOMDocument.6.0");
				  xslTemplate=new ActiveXObject("Msxml2.XSLTemplate.6.0");
				  xslDoc.async=false;
				  xslDoc.loadXML(transformObj.xml);
				  xslTemplate.stylesheet = xslDoc;
				  processorCache=xslTemplate.createProcessor();
			}
			else if (document.implementation && document.implementation.createDocument){
				var xsltProcessor = new XSLTProcessor();
				xsltProcessor.importStylesheet(transformObj);
				var fragment = xsltProcessor.transformToFragment(transformObj, document);
				return xsltProcessor;
			}
		 }
		 return processorCache;
		}
		function loadXML(sourceObj){
			var t_XmlDoc=null;
			if (window.ActiveXObject){
				t_XmlDoc=new ActiveXObject("Msxml2.DOMDocument.6.0");
				t_XmlDoc.async=false;
				t_XmlDoc.load(sourceObj);
				if (t_XmlDoc.parseError.errorCode!=0){
					document.write("Error in XML<br><br>Line " + t_XmlDoc.parseError.line + ": " + t_XmlDoc.parseError.reason);			
					alert("Error in XML\n\nLine " + t_XmlDoc.parseError.line + ": " + t_XmlDoc.parseError.reason);
					return null
				}
			}
			else if (!(window.ActiveXObject) && ("ActiveXObject" in window))
			{
				t_XmlDoc=new ActiveXObject("Msxml2.DOMDocument");
				t_XmlDoc.async=false;
				t_XmlDoc.load(sourceObj);
			}
			else if (document.implementation && document.implementation.createDocument){
				var myXMLHTTPRequest = new XMLHttpRequest();
				myXMLHTTPRequest.open("GET", sourceObj, false);
				myXMLHTTPRequest.send(null);
				t_XmlDoc = myXMLHTTPRequest.responseXML;
				//t_XmlDoc=document.implementation.createDocument("","",null);
				//t_XmlDoc.async=false;
				//t_XmlDoc.load("book1.xml")
				//xslStylesheet = myXMLHTTPRequest.responseXML;
				//xsltProcessor.importStylesheet(xslStylesheet);
				//xmlDoc = myXMLHTTPRequest.responseXML;
				//var xsltProcessor = new XSLTProcessor();
				//var fragment = xsltProcessor.transformToFragment(xmlDoc, document);
			}
			return t_XmlDoc;
		}

		function transformData(srcDoc,processor){
			 processor.input=srcDoc;
			 processor.transform();
			 return processor.output;
		}

		function mainPage(genre){
			if ("ActiveXObject" in window)
			{	
				 var txt, processor=getProcessor(xslDoc);
				 //processor.addParameter("selected_genre",genre);
				 txt=transformData(xmlDoc,processor);
				 //h_parser = new DOMParser()
                 //h_doc = h_parser.parseFromString(xmlString, "text/xml");
				 var htmlObject = document.createElement('div');
				 htmlObject.innerHTML = txt;
				 document.getElementById("xslOut").innerHTML = "";
				 document.getElementById("xslOut").innerHTML=htmlObject.getElementsByTagName("div")("xslOut").innerHTML;
				 htmlObject = null;
				 //document.getElementsByTagName("html")[0].innerHTML= "";
			}
			else if (document.implementation && document.implementation.createDocument){
				
				 var xsltPrs=new XSLTProcessor();
				 xsltPrs.importStylesheet(xslDoc);
				 //xsltPrs.setParameter(null, "selected_genre", genre);
				 var result=xsltPrs.transformToFragment(xmlDoc,document);
				 //document.getElementById("xslOut").innerHTML="";
				 result.childNodes[0].innerHTML = result.childNodes[0].innerHTML.replace(/~!~/g, "<br/>");
				 //document.getElementById("xslOut").appendChild(result);
				 document.childNodes[0].innerHTML = result.childNodes[0].innerHTML;
			}
		}
		function AlterResultTableColorIE()
			{	
				  for(var i=1;i<document.getElementById("ResultTable").rows.length;i++)
				  {
					var cellDEs = document.getElementById("ResultTable").rows[i].cells(1);
					cellDEs.innerText = cellDEs.innerText.replace(/~!~/g, "\r\n");
					var cellExpected = document.getElementById("ResultTable").rows[i].cells(2);
					cellExpected.innerText = cellExpected.innerText.replace(/~!~/g, "\r\n");
					var cellErrorMessage = document.getElementById("ResultTable").rows[i].cells(3);
					cellErrorMessage.innerText = cellErrorMessage.innerText.replace(/~!~/g, "\r\n");
					
					var cellManualCheck = document.getElementById("ResultTable").rows[i].cells(4);
					cellManualCheck.innerText = cellManualCheck.innerText.replace(/~!~/g, "\r\n");
					tempLinkNumber = 1;
					while (cellManualCheck.innerText.split("#$#").length > 2) // include link mark #$#
					{
						tmpLink = cellManualCheck.innerText.split("#$#")[1]
						//alert(cellManualCheck.innerText);
						//cellManualCheck.innerHTML = cellManualCheck.innerHTML.replace(tmpLink, "Click->Link" + tempLinkNumber +"   ")
						comment = tmpLink.split("@@@")[0]
						link = tmpLink.split("@@@")[1]
						cellManualCheck.innerHTML = cellManualCheck.innerHTML.replace(tmpLink, tempLinkNumber + ".   " + comment)
						//cellManualCheck.innerHTML = cellManualCheck.innerHTML.replace(tmpLink, comment)
						cellManualCheck.innerHTML = cellManualCheck.innerHTML.replace("#$#", "<a href=\""+link+"\" target=\"_Blank\">")
						cellManualCheck.innerHTML = cellManualCheck.innerHTML.replace("#$#", "</a>")
						tempLinkNumber++;
					}
					var cellColor = document.getElementById("ResultTable").rows[i].cells(5);
					var row = document.getElementById("ResultTable").rows[i];
					var oddEven = i % 2;
						if (oddEven == 0) 
						{ 
							row.style.backgroundColor="#FFEFAD";
						} 
						else if (oddEven ==1) 
						{ 
							row.style.backgroundColor="#FFFFFF";
						} 
						if(cellColor.innerText == "Pass")
						{
						  cellColor.style.backgroundColor="lime";
						  cellColor.style.color="black";
						  cellColor.style.fontWeight="bold";
						}
						else if(cellColor.innerText == "Block")
						{
						  cellColor.style.backgroundColor="royalblue";
						  cellColor.style.color="white";
						  cellColor.style.fontWeight="bold";
						}
						else if(cellColor.innerText == "Fail")
						{
						  cellColor.style.backgroundColor="red";
						  cellColor.style.color="white";
						  cellColor.style.fontWeight="bold";
						}
						else if(cellColor.innerText == "TBD")
						{
						  cellColor.style.backgroundColor="white";
						  cellColor.style.color="Black";
						  cellColor.style.fontWeight="bold";
						}
				   }
			}
		function toBreak(str, intLen)
		{
			var stmp = "";
			while(str.length > intLen){
				stmp += str.substr(0, intLen) + " ";
				str = str.substr(intLen, str.length);
			}
			stmp += " " + str;
			return stmp;
		}
		function AlterResultTableColorFF()
		{	
			  for(var i=1;i<document.getElementById("ResultTable").rows.length;i++)
			  {
				var cellDEs = document.getElementById("ResultTable").rows[i].cells[1];
				cellDEs.innerHTML = cellDEs.innerHTML.replace(/~!~/g, "<br/>");
				var cellExpected = document.getElementById("ResultTable").rows[i].cells[2];
				cellExpected.innerHTML = cellExpected.innerHTML.replace(/~!~/g, "<br/>")
				var cellErrorMessage = document.getElementById("ResultTable").rows[i].cells[3];
				cellErrorMessage.innerHTML = cellErrorMessage.innerHTML.replace(/~!~/g, "<br/>");
				var cellManualCheck = document.getElementById("ResultTable").rows[i].cells[4];
				cellManualCheck.innerHTML = cellManualCheck.innerHTML.replace(/~!~/g, "<br/>");
				tempLinkNumber = 1;
				while (cellManualCheck.textContent.split("#$#").length > 2) // include link mark #$#
				{
						tmpLink = cellManualCheck.innerText.split("#$#")[1]
						//cellManualCheck.innerHTML = cellManualCheck.innerHTML.replace(tmpLink, "Click->Link" + tempLinkNumber +"   ")
						comment = tmpLink.split("@@@")[0]
						link = tmpLink.split("@@@")[1]
						cellManualCheck.innerHTML = cellManualCheck.innerHTML.replace(tmpLink, tempLinkNumber + ".   " + comment)
						//cellManualCheck.innerHTML = cellManualCheck.innerHTML.replace(tmpLink, comment)
						cellManualCheck.innerHTML = cellManualCheck.innerHTML.replace("#$#", "<a href=\""+link+"\" target=\"_Blank\">")
						cellManualCheck.innerHTML = cellManualCheck.innerHTML.replace("#$#", "</a>")
						tempLinkNumber++;
				}
				var cellColor = document.getElementById("ResultTable").rows[i].cells[5];
				var row = document.getElementById("ResultTable").rows[i];
				var oddEven = i % 2;
					if (oddEven == 0) 
					{ 
						row.style.backgroundColor="#FFEFAD";
					} 
					else if (oddEven ==1) 
					{ 
						row.style.backgroundColor="#FFFFFF";
					} 
					if(cellColor.textContent == "Pass")
					{
					  cellColor.style.backgroundColor="lime";
					  cellColor.style.color="black";
					  cellColor.style.fontWeight="bold";
					}
					else if(cellColor.textContent == "Block")
					{
					  cellColor.style.backgroundColor="royalblue";
					  cellColor.style.color="white";
					  cellColor.style.fontWeight="bold";
					}
					else if(cellColor.textContent == "Fail")
					{
					  cellColor.style.backgroundColor="red";
					  cellColor.style.color="white";
					  cellColor.style.fontWeight="bold";
					}
					else if(cellColor.textContent == "TBD")
					{
					  cellColor.style.backgroundColor="white";
					  cellColor.style.color="Black";
					  cellColor.style.fontWeight="bold";
					}
			   }
		}
		function AlterResultTableColor()
		{
			if (window.ActiveXObject){
				AlterResultTableColorIE();
			}
			else if (!(window.ActiveXObject) && ("ActiveXObject" in window))
			{
				AlterResultTableColorIE();
			}
			else if (document.implementation && document.implementation.createDocument){
				AlterResultTableColorFF();
			}
		}
		
		function HoverOut(row)
		{
			var oddEven = row.rowIndex % 2;
				if (oddEven == 0) 
				{ 
					row.style.backgroundColor="#FFEFAD";
				} 
				else if (oddEven ==1) 
				{ 
					row.style.backgroundColor="#FFFFFF";
				} 
		}
		function HoverColor(row)
		{
			var cellColor = row.cells[row.cells.length - 1];
			if ("ActiveXObject" in window)
			{
				if(cellColor.innerText == "Pass")
				{
				  row.style.backgroundColor="#CEFFCE";
				}
				else if(cellColor.innerText == "Block")
				{
				  row.style.backgroundColor="#ADF7FF";
				}
				else if(cellColor.innerText == "Fail")
				{
				  row.style.backgroundColor="#FFC3C6";
				}
			}
			else if (document.implementation && document.implementation.createDocument){
				
				if(cellColor.textContent == "Pass")
				{
				  row.style.backgroundColor="#CEFFCE";
				}
				else if(cellColor.textContent == "Block")
				{
				  row.style.backgroundColor="#ADF7FF";
				}
				else if(cellColor.textContent == "Fail")
				{
				  row.style.backgroundColor="#FFC3C6";
				}
			}
		}
		document.onreadystatechange=function(){
		 if(document.readyState==="complete"){
			  xslDoc=loadXML("xmlReport.xsl");
			  //xmlDoc=loadXML(window.location);
			  //xmlDoc is used for paging. And can not use "window.location"(file:\\ss), it need to be splited as x.xml.
			  //mainPage("all");
		 }
		};
		function LoadXMLResult()
		{
		  //xslDoc=loadXML("book.xsl");
		  //xmlDoc=loadXML("book.xml");
		  //mainPage("all");
		}
		]]>
				

	</script>
	</head>
	<body onload="AlterResultTableColor();">
	 <!--><h1>Test Project</h1><-->
	 <div id="xslOut">
		<table border="2" bordercolor="black" class="ResultTable">
			<tr class="TrTh">
				<td>Project</td>
				<td>Test Name</td>
				<td>Model</td>
				<td>OS</td>
				<td>OS Version</td>
				<td>Language</td>
				<td>Time(s)</td>
			</tr>
			<tr class="TrResult1" >
				<td><xsl:value-of select= "testsuite/@project"/></td>
				<td><xsl:value-of select= "testsuite/@testName"/></td>
				<td><xsl:value-of select= "testsuite/@deviceModel"/></td>
				<td><xsl:value-of select= "testsuite/@os"/></td>
				<td><xsl:value-of select= "testsuite/@version"/></td>
				<td><xsl:value-of select= "testsuite/@language"/></td>
				<td><xsl:value-of select= "testsuite/@time"/></td>
			</tr>
		</table>
		<table id="Table_FailPassRate" border="2" bordercolor="black" class="SummaryTable">
			<tr class="SummaryResult">
				<td class="TdFailPassRateTitle">Total:</td>
				<td style="font-weight:bold;background-color:#FFEFAD;width=100;" ><xsl:value-of select= "testsuite/@tests"/></td>
				<td class="TdFailPassRateTitle">Passed:</td>
				<td style="font-weight:bold;background-color:lime;width=70;" ><xsl:value-of select= "testsuite/@passes"/></td>
				<td style="font-weight:bold;background-color:lime;width=70;" ><xsl:value-of select= "testsuite/@passesPercent"/></td>
				<td class="TdFailPassRateTitle">Failed:</td>
				<td style="font-weight:bold;background-color:red;width=70;" ><xsl:value-of select= "testsuite/@failures"/></td>
				<td style="font-weight:bold;background-color:red;width=70;" ><xsl:value-of select= "testsuite/@failsPercent"/></td>
				<td class="TdFailPassRateTitle">Blocked:</td>
				<td style="font-weight:bold;background-color:royalblue;width=70;" ><xsl:value-of select= "testsuite/@blocks"/></td>
				<td style="font-weight:bold;background-color:royalblue;width=70;" ><xsl:value-of select= "testsuite/@blocksPercent"/></td>
				<td class="TdFailPassRateTitle">TBD:</td>
				<td style="font-weight:bold;width=70;"><xsl:value-of select= "testsuite/@tbds"/></td>
				<td style="font-weight:bold;width=70;"><xsl:value-of select= "testsuite/@tbdsPercent"/></td>
			</tr>
		</table>		
		<xsl:apply-templates select="testsuite">
		</xsl:apply-templates>
	 </div> 
	 </body>
	 </html>
	 </xsl:template>
	 <xsl:template match="testsuite">
		<table id="ResultTable" border="2" bordercolor="black" class="ResultTable">
			<colgroup>
				<col id="colStep"/>
				<col id="colDescription"/>
				<col id="colExpectedResult"/>
				<col id="colErrorMessage"/>
				<col id="colManualCheck"/>
				<col id="colResult"/>
				<col id="colTime"/>
			</colgroup>
			<tr class="TrTh">
				<td id="tdStep">Step</td>
				<td id="tdDescription">Description</td>
				<td id="tdExpectedResult">Expected Result</td>
				<td id="tdErrorMessage">Error Message</td>
				<td id="tdManualCheck">Need To Check</td>
				<td id="tdResult" style="cursor:hand;">Result</td>
				<td id="tdTime" style="cursor:hand;">Time(s)</td>
			</tr>
			<!--><xsl:apply-templates select="Report[Result='Fail'][position()&gt;=1 and position()&lt;=4]"><-->
			<xsl:apply-templates select="testcase[position()&gt;=1 and position()&lt;=400]">
				<!--><xsl:sort data-type="text" select="DateTime" order="descending"/><-->
				<xsl:sort data-type="number" select="Step" order="ascending"/>
			</xsl:apply-templates>
		</table>
	</xsl:template>
	<xsl:template match="testsuite/testcase">
		<tr onmouseover="HoverColor(this);" onmouseout="HoverOut(this);">
			<td nowrap="nowrap" style="text-align:center;">
				<xsl:value-of select="step"/>
			</td>
			<td style="Width:600;text-align:left;">
				<xsl:value-of select="description"/>
			</td>
			<td style="Width:600;text-align:left;">
				<xsl:value-of select="expectedResult"/>
			</td>
			<td style="Width:300;text-align:left;">
				<xsl:value-of select="failure/@message"/>
			</td>
			<td style="Width:300;text-align:left;">
				<xsl:value-of select="needToCheck"/>
			</td>
			<td ondblclick="CreateDataTable('Result', this);" style="text-align:center;cursor:hand;" nowrap="nowrap">
				<xsl:value-of select="result"/>
			</td>
			<td style="text-align:center;">
				<xsl:value-of select="@time"/>
			</td>
		</tr>
	</xsl:template>
</xsl:stylesheet>