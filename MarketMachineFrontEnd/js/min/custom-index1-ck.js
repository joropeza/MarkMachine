var $border_color="#efefef",$grid_color="#ddd",$default_black="#666",$red="#eb4343",$blue="#5da4cd",$green="#abd14f",$yellow="#ffaa3a",$yellow_one="#FFF844",$grey="#999999",$blue_one="#74b1d4",$blue_two="#84bad9",$blue_three="#9bc7e0",$blue_four="#afd2e6",$blue_five="#badff2";$(document).ready(function(){$(".knob").knob({change:function(t){},release:function(t){console.log("release : "+t)},cancel:function(){console.log("cancel : ",this)},draw:function(){if("tron"==this.$.data("skin")){this.cursorExt=.3;var t=this.arc(this.cv),e,i=1;return this.g.lineWidth=this.lineWidth,this.o.displayPrevious&&(e=this.arc(this.v),this.g.beginPath(),this.g.strokeStyle=this.pColor,this.g.arc(this.xy,this.xy,this.radius-this.lineWidth,e.s,e.e,e.d),this.g.stroke()),this.g.beginPath(),this.g.strokeStyle=i?this.o.fgColor:this.fgColor,this.g.arc(this.xy,this.xy,this.radius-this.lineWidth,t.s,t.e,t.d),this.g.stroke(),this.g.lineWidth=2,this.g.beginPath(),this.g.strokeStyle=this.o.fgColor,this.g.arc(this.xy,this.xy,this.radius-this.lineWidth+1+2*this.lineWidth/3,0,2*Math.PI,!1),this.g.stroke(),!1}}})}),$(function(){$("#memory").sparkline([899,99,68,87,45],{type:"pie",sliceColors:[$green,$yellow,$blue,$red,$yellow_one],width:"160px",height:"160px"}),$("#dow").sparkline([5,6,7,9,9,5,3,2,4,5,6,7,9,9,5,3,2,2,4,5,6,7,9,9,5,3,2,2,4,5,6,7,9,9],{height:"80",type:"bar",barSpacing:7,barWidth:12,barColor:"#e5f3fa",tooltipPrefix:"Volume: "}),$("#dow").sparkline([0,90,290,480,390,420,0,120,250,128,250,213,150,60,280,389,150,90,250,400,280],{composite:!0,height:"80",fillColor:!1,lineWidth:1,highlightSpotColor:"#333",spotColor:"#eb4343",minSpotColor:"#eb4343",maxSpotColor:"#eb4343",spotRadius:4,lineColor:"#eb4343",tooltipPrefix:"Index: "})}),$(document).ready(function(){$("#data-table").dataTable({sPaginationType:"full_numbers"}),$("#data-table_length").css("display","none")});