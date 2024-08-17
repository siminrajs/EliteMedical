$(document).ready(function() {
  $('.owl-carousel.banner').owlCarousel({
    loop: false,
    autoplay: true,  
    autoplaySpeed:1000, 
    dots:false,  
    autoplayTimeout:8000,
    nav: true,
    navText:["<div class='nav-btn prev-slide'></div>","<div class='nav-btn next-slide'></div>"],
    responsiveClass: true,
    responsive: {
      0: {
        items: 1,
        nav: false
      },
      600: {
        items: 1,
        nav: false
      },
      1000: {
        items: 1,
        nav: true,
        loop: true
      }
    }
  })
})


$(document).ready(function() {
  $('.owl-carousel.hslidr-6').owlCarousel({
      loop: false,
      autoplay: false,
      smartSpeed: 300,
      dots: false,
      autoplayHoverPause: true,
      margin: 20,
      responsiveClass: true,
      responsive: {
          0: {
              items: 1,
              nav: false
          },
          420: {
              items: 2,
              nav: false
          },
          600: {
              items: 3,
              nav: false
          },
          1000: {
              items: 4,
              nav: false
          },
          1400: {
              items: 5,
              nav: false

          }
      }
  })
})


$(document).ready(function() {
  $('.owl-carousel.hm-dr-slider').owlCarousel({
      loop: false,
      autoplay: false,
      smartSpeed: 300,
      dots: true,
      autoplayHoverPause: true,
      margin: 30,
      responsiveClass: true,
      responsive: {
          0: {
              items: 1,
              nav: false
          },
          420: {
              items: 1,
              nav: false
          },
          600: {
              items: 3,
              nav: false
          },
          1000: {
              items: 4,
              nav: false
          },
          1440: {
              items: 5,
              nav: false

          }
      }
  })
})

$(document).ready(function() {
  $('.owl-carousel.dpt-dr-slider').owlCarousel({
      loop: false,
      autoplay: false,
      smartSpeed: 300,
      dots: true,
      autoplayHoverPause: true,
      margin: 20,
      responsiveClass: true,
      responsive: {
          0: {
              items: 1,
              nav: false
          },
          420: {
              items: 1,
              nav: false
          },
          600: {
              items: 3,
              nav: false
          },
          1000: {
              items: 4,
              nav: false
          },
          1400: {
              items: 5,
              nav: false

          }
      }
  })
})


$(document).ready(function() {
  $('.owl-carousel.hm-client-slider').owlCarousel({
      loop: false,
      autoplay: false,
      smartSpeed: 300,
      dots: false,
      autoplayHoverPause: true,
      margin: 20,
      responsiveClass: true,
      responsive: {
          0: {
              items: 2,
              nav: false
          },
          420: {
              items: 2,
              nav: false
          },
          600: {
              items: 3,
              nav: false
          },
          1000: {
              items: 5,
              nav: false
          },
          1400: {
              items: 5,
              nav: false

          }
      }
  })
})

$(document).ready(function() {
  $('.owl-carousel.testmonial').owlCarousel({
    loop: false,
    autoplay:true,   
    dots:true,  
    smartSpeed:750,
    nav: true,
    navText:["<div class='nav-btn prev-slide'></div>","<div class='nav-btn next-slide'></div>"],
    responsiveClass: true,
    responsive: {
      0: {
        items: 1,
        nav: false
      },
      600: {
        items: 1,
        nav: false
      },
      1000: {
        items: 1,
        nav: true,
        loop: true
      }
    }
  })
})

$(document).ready(function() {
  $('.owl-carousel.hm-dpt-slider').owlCarousel({
      loop: false,
      autoplay: false,
      smartSpeed: 300,
      dots: false,
      autoplayHoverPause: true,
      margin: 30,
      nav: true,
      navText:["<div class='nav-btn prev-slide'></div>","<div class='nav-btn next-slide'></div>"],
      responsiveClass: true,
      responsive: {
          0: {
              items: 1,
              nav: false
          },
          420: {
              items: 1,
              nav: false
          },
          600: {
              items: 3,
              nav: true
          },
          1000: {
              items: 4,
              nav: true
          }         
      }
  })
})
  
$('.owl_1').owlCarousel({
loop:false,
margin:25,	
responsiveClass:true,
autoHeight:true,
autoplay:false,
dots: false,
nav: true,
    navText:["<div class='nav-btn prev-slide'></div>","<div class='nav-btn next-slide'></div>"],
  responsive:{
      0:{
          items:2,
          nav:false,
      loop:false,
      autoplay:false,
      },
      600:{
          items:3,
          nav:false,
      loop:false,
      margin:15,
      autoplay:false,	
      },
      900:{
        items:3,
        nav:true,
    loop:false,
    margin:15,	
    },
      1000:{
          items:5,
          nav:true,
          loop:false
      },
      1440:{
        items:5,
        nav:true,
        loop:false
    }
  }
})

$(document) .ready(function(){
  $(".owl-item").click(function(){
    $(".nav-link").removeClass('active');
  });
  });


//select doctors
$(document).ready(function(){
    $(".dr-slBx").change(function(){
        $(this).find("option:selected").each(function(){
            var optionValue = $(this).attr("value");
            if(optionValue){
                $(".box").not("." + optionValue).hide();
                $("." + optionValue).show();
            } else{
                $(".box").hide();
            }
        });
    }).change();
});


function openNav() {
  document.getElementById("main-menu-top").style.width = "100%";
  document.getElementById("main-menu-top").style.left = "0px";
}

function closeNav() {
  document.getElementById("main-menu-top").style.width = "0";
  document.getElementById("main-menu-top").style.left = "-50px";
}


if ($(window).width() < 1025) {
$(".hm-specialization").prepend($(".bnr-fnd-dr"));
}
if ($(window).width() < 767) {
$(".main-menu").append($(".header-top"));
}
if ($(window).width() < 767) {
  $("#main-tab").remove();
} else 
{
  $("#mob-tab").remove();
}

if ($(window).width() < 768) {
  $('.ftr-col-2 ul').hide();
  $('.ftr-col-3 ul').hide();
  $('.ftr-col-1 p').hide();
  $(".ftr-col-2 h3").append("<span class='drop_down_icon fa fa-angle-down'></span>");
  $(".ftr-col-3 h3").append("<span class='drop_down_icon fa fa-angle-down'></span>");
  $(".ftr-col-1 h4").append("<span class='drop_down_icon fa fa-angle-down'></span>");
  $('.ftr-col-2 h3').click(function() {
    $(this).closest('.ftr-col-2').find("ul").slideToggle();
});
  $('.ftr-col-3 h3').click(function() {
    $(this).closest('.ftr-col-3').find("ul").slideToggle();
});
  $('.ftr-col-1 h4').click(function() {
    $(this).closest('.ftr-col-1').find("p").slideToggle();
});

}


$('.slider').slick({
  slidesToShow: 3,
  slidesToScroll: 1,
  arrows: true,
  dots: true,
  centerMode: true,
  variableWidth: true,
  infinite: true,
  focusOnSelect: true,
  cssEase: 'linear',
  touchMove: true,
  prevArrow:'<button class="slick-prev">  </button>',
  nextArrow:'<button class="slick-next">  </button>',
  
  //         responsive: [                        
  //             {
  //               breakpoint: 576,
  //               settings: {
  //                 centerMode: false,
  //                 variableWidth: false,
  //               }
  //             },
  //         ]
});


var imgs = $('.slider img');
imgs.each(function(){
  var item = $(this).closest('.item');
  item.css({
    'background-image': 'url(' + $(this).attr('src') + ')', 
    'background-position': 'center',            
    '-webkit-background-size': 'cover',
    'background-size': 'cover', 
  });
  $(this).hide();
});


$('.dpt-bnr').owlCarousel({
  margin:10,
  loop:false,
  dots: false,
  nav: false,
  autoWidth:true,
  items:4,
  margin: 20
})



