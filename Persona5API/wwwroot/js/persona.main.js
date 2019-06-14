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
			this.randomPersonas();
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
		},

		randomPersonas: function () {
			new Vue({
				el: ".random-persona",
				data() {
					return {
						random: {},
						loading: true,
						errored: false
					}
				},
				mounted: function () {
					this.getRandom()
				},
				methods: {
					getPersonas() {
						axios.get("api/personas")
							.then(response => { this.persona = response.data })
					},
					async getRandom() {
						await axios.get("api/personas/random")
							.then(response => { this.random = response.data })
							
							.finally(() => {
								this.loading = false;

							})
					}
                },
                filters: {
                    uppercase: function (value) {
                        if (!value) return ''
                        value = value.toString()
                        return value.charAt(0).toUpperCase() + value.slice(1)
                    }
                }
			});
		}
	};
	return main;
})(jQuery);

$(function () {
	persona.main.init();
});