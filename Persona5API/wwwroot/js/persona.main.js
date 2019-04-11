// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
if (typeof persona === "undefined") {
	var persona = {};
}

persona.main = (function ($) {
	var main = {

		init: function () {

			this.skillSelectList();
		},
		skillSelectList: function () {
			// Enable live search
			$('#SkillsList').attr('data-live-search', true);

			$('#SkillsList').attr('multiple', true);
			$('#SkillsList').attr('data-selected-text-format', 'count');

			$('.selectSkills, .selectElements').selectpicker({
				width: 'auto',
				title: '--- Choose Multiple Skills ---',
				size: 6,
				iconBase: 'fa',
				tickIcon: 'fa-check'
			});
		}
	};
	return main;
})(jQuery);

$(function () {
	persona.main.init();
});