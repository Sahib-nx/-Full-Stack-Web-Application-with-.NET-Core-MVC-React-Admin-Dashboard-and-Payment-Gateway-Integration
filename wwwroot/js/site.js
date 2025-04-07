// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function() {
    const navbarToggle = document.getElementById('navbar-toggle');
    const navbarCollapse = document.getElementById('navbar-collapse');
    const searchToggle = document.getElementById('search-toggle');
    const searchContainer = document.getElementById('search-container');
    const searchClose = document.getElementById('search-close');
    
    // Toggle mobile menu
    navbarToggle.addEventListener('click', function() {
        navbarCollapse.classList.toggle('active');
        this.classList.toggle('active');
        
        // Transform hamburger to X
        const bars = this.querySelectorAll('.bar');
        if (this.classList.contains('active')) {
            bars[0].style.transform = 'rotate(45deg) translate(5px, 6px)';
            bars[1].style.opacity = '0';
            bars[2].style.transform = 'rotate(-45deg) translate(5px, -6px)';
        } else {
            bars[0].style.transform = 'none';
            bars[1].style.opacity = '1';
            bars[2].style.transform = 'none';
        }
    });
    
    // Toggle search bar
    searchToggle.addEventListener('click', function(e) {
        e.preventDefault();
        searchContainer.classList.toggle('active');
        document.querySelector('.search-input').focus();
    });
    
    // Close search bar
    searchClose.addEventListener('click', function() {
        searchContainer.classList.remove('active');
    });
    
    // Close mobile menu when window is resized
    window.addEventListener('resize', function() {
        if (window.innerWidth > 992 && navbarCollapse.classList.contains('active')) {
            navbarCollapse.classList.remove('active');
            navbarToggle.classList.remove('active');
            
            const bars = navbarToggle.querySelectorAll('.bar');
            bars[0].style.transform = 'none';
            bars[1].style.opacity = '1';
            bars[2].style.transform = 'none';
        }
    });
    
    // Add scroll effect
    window.addEventListener('scroll', function() {
        const navbar = document.querySelector('.navbar-main');
        if (window.scrollY > 50) {
            navbar.style.backgroundColor = '#ffffff';
            navbar.style.boxShadow = '0 2px 10px rgba(0, 0, 0, 0.1)';
        } else {
            navbar.style.backgroundColor = 'var(--secondary)';
            navbar.style.boxShadow = '0 2px 10px rgba(0, 0, 0, 0.1)';
        }
    });
    
    // Add animation to nav links
    const navLinks = document.querySelectorAll('.nav-link');
    navLinks.forEach((link, index) => {
        link.style.animation = `fadeIn 0.5s ease forwards ${index / 7 + 0.2}s`;
        link.style.opacity = '0';
    });
    
    // Add keydown event for accessibility
    document.addEventListener('keydown', function(e) {
        if (e.key === 'Escape') {
            searchContainer.classList.remove('active');
            if (navbarCollapse.classList.contains('active')) {
                navbarCollapse.classList.remove('active');
                navbarToggle.classList.remove('active');
                
                const bars = navbarToggle.querySelectorAll('.bar');
                bars[0].style.transform = 'none';
                bars[1].style.opacity = '1';
                bars[2].style.transform = 'none';
            }
        }
    });
});