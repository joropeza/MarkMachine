var $border_color = "#efefef";
var $grid_color = "#ddd";
var $default_black = "#666";
var $red = "#FF6464";
var $blue = "#53ACDF";
var $green = "#abd14f";
var $yellow = "#ffaa3a";
var $grey = "#999999";
var $blue_one = "#74b1d4";
var $blue_two = "#84bad9";
var $blue_three = "#9bc7e0";
var $blue_four = "#afd2e6";
var $blue_five = "#badff2";

$(function () {

	var data, chartOptions;
	
	data = [
		{ label: "Windows", data: Math.floor (Math.random() * 100 + 150) }, 
		{ label: "Mac", data: Math.floor (Math.random() * 100 + 390) }, 
		{ label: "Vista", data: Math.floor (Math.random() * 100 + 530) }, 
		{ label: "Linux", data: Math.floor (Math.random() * 100 + 90) },
		{ label: "Mobile", data: Math.floor (Math.random() * 100 + 320) }
	];

	chartOptions = {        
		series: {
			pie: {
				show: true,  
				innerRadius: .4, 
				stroke: {
					width: 1,
				}
			}
		}, 
		shadowSize: 0,
		legend: {
			show: false,
		},
		
		tooltip: true,

		tooltipOpts: {
			content: '%s: %y'
		},
		
		grid:{
      hoverable: true,
      clickable: false,
      borderWidth: 0,
			tickColor: '#fff',
      borderColor: '#fff',
    },
    shadowSize: 0,
		colors: [$blue_one, $blue_two, $blue_three, $blue_four, $blue_five],
	};


	var holder = $('#donut-chart');

	if (holder.length) {
		$.plot(holder, data, chartOptions );
	}		
		
});