$(document).ready(function(){
	$(".has-submenu").append("<i class='ni ni-bold-down'></i>");
	$(".sub-menu").hide();
  $(".has-submenu a").click(function(){
	$(this).closest('.has-submenu').find(".sub-menu").slideToggle();
	$(this).closest('.has-submenu').find(".ni").toggleClass("ni-bold-up");
  });
})


	 	