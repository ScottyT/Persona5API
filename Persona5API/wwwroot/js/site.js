// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
	$('#SkillsList').attr('data-live-search', true);

	$('#SkillsList').attr('multiple', true);
	$('#SkillsList').attr('data-selected-text-format', 'count');

	$('.selectSkills').selectpicker({
		width: 'auto',
		title: '--- Choose Multiple Skills ---',
		size: 6,
		iconBase: 'fa',
		tickIcon: 'fa-check'
	});

	$('.selectElements').selectpicker({
		width: 'auto',
		title: '--- Choose Multiple Elements ---',
		size: 6,
		iconBase: 'fa',
		tickIcon: 'fa-check'
	});
});