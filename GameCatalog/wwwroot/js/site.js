// Плавная прокрутка для навигационных ссылок
document.querySelectorAll('a.nav-link').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        const targetId = this.getAttribute('href');
        if (targetId.startsWith('#')) {
            const targetElement = document.querySelector(targetId);
            if (targetElement) {
                targetElement.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        } else {
            window.location.href = this.href;
        }
    });
});

// Анимация появления элементов при скролле
const observerOptions = {
    threshold: 0.1,
    rootMargin: '0px 0px -50px 0px'
};

const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.classList.add('fade-in');
            observer.unobserve(entry.target);
        }
    });
}, observerOptions);

// Наблюдаем за элементами с задержкой появления
document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.card, .feature-icon').forEach((el, index) => {
        el.style.animationDelay = `${index * 0.1}s`;
        observer.observe(el);
    });
});

const searchBox = document.getElementById("searchBox");
const gamesContainer = document.getElementById("gamesContainer");

searchBox.addEventListener("input", async () => {
    const term = searchBox.value.trim();

    const response = await fetch(`/Games/Search?term=${encodeURIComponent(term)}`);
    const data = await response.json();

    gamesContainer.innerHTML = "";

    if (data.length === 0) {
        gamesContainer.innerHTML = "<p class='text-center text-light'>Ничего не найдено 😢</p>";
        return;
    }

    data.forEach(game => {
        const card = document.createElement("div");
        card.className = "col-md-4 mb-3";
        card.innerHTML = `
            <div class="card h-100 shadow-sm">
                <img src="${game.imageUrl}" class="card-img-top" alt="${game.title}" />
                <div class="card-body d-flex flex-column">
                    <h5 class="card-title">${game.title}</h5>
                    <p class="card-text">${game.developer} (${game.releaseYear})</p>
                    <p class="card-text">Жанр: ${game.genre}</p>
                    <p class="card-text description flex-grow-1">${game.description}</p>
                    <a href="/Games/Details?id=${game.id}" class="btn btn-primary mt-auto">Подробнее</a>
                </div>
            </div>`;
        gamesContainer.appendChild(card);
    });
});