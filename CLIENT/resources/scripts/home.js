let myAccounts;
let myFurniture;
const accountUrl = "http://localhost:5178/api/account"
const furnitureUrl = "http://localhost:5178/api/furniture"
const orderUrl = "http://localhost:5178/api/order"
let myOrderForms;
let tempFurn;

async function handleBuyLoad()
{
    await getFurnitureData()
    displayBuyTable()
}

async function getAccountData()
{
    let response = await fetch(accountUrl)
    myAccounts = await response.json()
    console.log(myAccounts) // remove later
}

async function getFurnitureData()
{
    let response = await fetch(furnitureUrl)
    myFurniture = await response.json();
    console.log(myFurniture) // remove later
}

async function getOrderData()
{
    let response = await fetch(orderUrl)
    myOrderForms = await response.json()
    console.log(myOrderForms) // remove later
}
document.addEventListener('DOMContentLoaded', function() {
    const images = [
        "https://www.sne-furniture.com/uploads/imagegallery/images/Landing-Living3.jpg",
        "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        "https://cdn.mos.cms.futurecdn.net/hsYsUoZSsSGDdmkajRrBgK.jpeg",
    ];

    let currentImageIndex = 0;
    const slideshowElement = document.getElementById('slideshow');

    function updateSlideshow() {
        slideshowElement.src = images[currentImageIndex];
        currentImageIndex = (currentImageIndex + 1) % images.length; 
    }

    updateSlideshow();

    setInterval(updateSlideshow, 5000);
});

function shuffleArray(array) {
    for (let i = array.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [array[i], array[j]] = [array[j], array[i]];
    }
}

document.addEventListener('DOMContentLoaded', async function() {
    await fetchFurnitureData();
    displayRandomFurniture();
    setInterval(displayRandomFurniture, 6000); 
});

async function fetchFurnitureData() {
    const response = await fetch(furnitureUrl);
    myFurniture = await response.json();
}

function displayRandomFurniture() {
    if (!myFurniture || myFurniture.length === 0) return;
    shuffleArray(myFurniture);

    let html = '';
    for (let i = 0; i < myFurniture.length && i < 3; i++) {
        const furniture = myFurniture[i];
        if (!furniture.sold) {  
            html += `
                <div class="product-wrapper">
                    <h1>Product Details</h1>
                    <div>
                        <strong>Type:</strong> <span>${furniture.type}</span>
                    </div>
                    <div>
                        <strong>Price:</strong> <span>$${furniture.price}</span>
                    </div>
                    <div>
                        <img class="resize-image" src="${furniture.image}" alt="Product Image">
                    </div>
                    <button onclick="handleBuyClick('${furniture.id}')">Order</button>
                </div>
            `;
        }
    }
    document.getElementById('app').innerHTML = html;
}

function handleBuyClick(id)
{
    console.log(id)
    const furnTempUrl = furnitureUrl+"/"+id
    localStorage.setItem('furnTempUrl', furnTempUrl)
    console.log(furnTempUrl)

    window.location.href = "../resources/order.html"
}

async function handleOrderLoad()
{
    await getTempFurnData()
    await getAccountData()
    displayOrderForm()
}

async function handleSellLoad()
{
    await getAccountData()
}

async function getTempFurnData()
{
    let furnTempUrl = localStorage.getItem('furnTempUrl')
    console.log(furnTempUrl)
    let response = await fetch(furnTempUrl)
    tempFurn = await response.json()
    console.log(tempFurn) // remove later
}
