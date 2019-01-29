/**
 * jQuery Line Progressbar
 * Author: KingRayhan<rayhan095@gmail.com>
 * Author URL: http://rayhan.info
 * Version: 1.0.0
 */

(function($){
	'use strict';
	$.fn.LineProgressbar = function(options){
		var options = $.extend({
			percentage : null,
			ShowProgressCount: true,
			duration: 1000,
			// Styling Options
			fillBackgroundColor: '#3498db',
			backgroundColor: '#EEEEEE',
			radius: '0px',
			height: '10px',
			width: '100%',
			parentHeight:'40px'
		},options);
		return this.each(function(index, el) {
			// Markup
			console.log($(el));
			$(el).html('<div class="progressbar"><div class="proggress"></div><div class="percentCount"><div class="arrow"></div></div></div>');
			var percentCount = $(el).find('.percentCount');
			var progressFill = $(el).find('.proggress');
			var progressBar= $(el).find('.progressbar');
			$(el).css({
				height:options.parentHeight
			})
			progressFill.css({
				background : options.fillBackgroundColor,
				height : options.height,
				borderRadius: options.radius
			});
			progressBar.css({
				width : options.width,
				backgroundColor : options.backgroundColor,
				borderRadius: options.radius
			});
			// Progressing
			percentCount.animate({
				left:options.percentage + "%"
			},{
				duration: options.duration
			})
			progressFill.animate(
				{
					width: options.percentage + "%"
				},
				{	
					step: function(x) {
						if(options.ShowProgressCount){
							$(el).find(".percentCount").text(Math.round(x) + "%");
						}
					},
					duration: options.duration
				}
			);
		////////////////////////////////////////////////////////////////////
		});
	}
})(jQuery);