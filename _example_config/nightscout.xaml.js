// zoom in
var ivZoom = setInterval(function(){
	var li = document.querySelector('.focus-range li[data-hours="2"]');
	if (li) {
		clearInterval(ivZoom);
		li.click();
	}
}, 250);

// disable scrollbars
document.body.style.overflow = 'hidden';

// remove toolbar
var tb = document.querySelector('#toolbar');
if (tb)
	tb.remove();

// remove chart history scroller
var removeOrRetry = function() {
	var cc = document.querySelector('.chart-context');
	if (cc)
		cc.remove();
	else
		setTimeout(removeOrRetry, 250);
};
removeOrRetry();

// remove off-black background
var cc = document.querySelector('#chartContainer');
if (cc)
	cc.style.backgroundColor = 'black';

// reload every hour to free memory
setTimeout(() => location.href = location.href, 1000 * 60 * 60);

