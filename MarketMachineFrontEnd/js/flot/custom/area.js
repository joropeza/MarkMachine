var $border_color = "#efefef";
var $grid_color = "#ddd";
var $default_black = "#666";
var $red = "#FF6464";
var $blue = "#53ACDF";
var $green = "#abd14f";
var $yellow = "#ffaa3a";
var $grey = "#999999";

$(function () {
		
	var d1, data, chartOptions;

	d1 = [
		[1262304000000, 219], [1264982400000, 600], [1267401600000, 2123], [1270080000000, 2512], 
		[1272672000000, 1963], [1275350400000, 1905], [1277942400000, 1345], [1280620800000, 2917], 
		[1283299200000, 3200], [1285891200000, 1700], [1288569600000, 2199], [1291161600000, 1700]
	];

	d2 = [
		[1262304000000, 124], [1264982400000, 321], [1267401600000, 423], [1270080000000, 1813],
		[1272672000000, 554], [1275350400000, 667], [1277942400000, 448], [1280620800000, 1890], 
		[1283299200000, 1678], [1285891200000, 887], [1288569600000, 1264], [1291161600000, 1167]
	];

	data = [{ 
		label: "Likes", 
		data: d2
	}, {
		label: "Shares",
		data: d1
	}];

	chartOptions = {
		xaxis: {
			min: (new Date(2009, 12, 1)).getTime(),
			max: (new Date(2010, 11, 2)).getTime(),
			mode: "time",
			tickSize: [1, "month"],
			monthNames: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
			tickLength: 0
		},
		yaxis: {

		},
		series: {
			lines: {
				show: true, 
				fill: true,
				lineWidth: 1
			},
			points: {
				show: true,
				radius: 4.5,
				fill: true,
				fillColor: "#ffffff",
				lineWidth: 2
			}
		},
		grid:{
      hoverable: true,
      clickable: false,
      borderWidth: 1,
      tickColor: $border_color,
      borderColor: $grid_color,
    },
		legend: {
			show: true,
		},
		tooltip: true,
		tooltipOpts: {
			content: '%s: %y'
		},
		shadowSize: 0,
		colors: [$red, $blue, $green, $yellow, $grey],
	};

	var holder = $('#area-chart');

	if (holder.length) {
		$.plot(holder, data, chartOptions);
	}
});