document.addEventListener('DOMContentLoaded', function() {
    const password = document.getElementById('Password');
    const togglePassword = document.getElementById('togglePassword');
    
    // Toggle password visibility
    togglePassword.addEventListener('click', function() {
        const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
        password.setAttribute('type', type);
        this.querySelector('i').classList.toggle('fa-eye');
        this.querySelector('i').classList.toggle('fa-eye-slash');
    });
    
    // Add animation when fields get focus
    document.querySelectorAll('.form-control').forEach(input => {
        input.addEventListener('focus', function() {
            this.style.transform = 'translateX(5px)';
            setTimeout(() => {
                this.style.transform = 'translateX(0)';
            }, 300);
        });
    });
    
    // Show success/error message animation
    const successMsg = document.querySelector('.success');
    const errorMsg = document.querySelector('.error');
    
    if (successMsg) {
        // Auto hide success message after 5 seconds
        setTimeout(() => {
            successMsg.style.animation = 'slideUp 0.5s ease-in-out forwards';
            setTimeout(() => {
                successMsg.style.display = 'none';
            }, 500);
        }, 5000);
    }
    
    // Add keyframe for slideUp
    const style = document.createElement('style');
    style.textContent = `
        @keyframes slideUp {
            from { opacity: 1; transform: translateY(0); }
            to { opacity: 0; transform: translateY(-20px); }
        }
    `;
    document.head.appendChild(style);
});