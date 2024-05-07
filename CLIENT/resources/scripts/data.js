let myAccounts;
let myFurniture;
let myOrderForms;
const accountUrl = "http://localhost:5178/api/account"
const furnitureUrl = "http://localhost:5178/api/furniture"
const orderUrl = "http://localhost:5178/api/order"
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

function displayBuyTable() {
    let html = '';
    myFurniture.forEach((furniture) => {
        if (!furniture.sold) {  
            html += `
                <div class="product-wrapper">
                    <h1>Product Details</h1>
                    <div>
                        <strong>Type:</strong> <span>${furniture.type}</span>
                    </div>
                    <div>
                        <strong>Condition:</strong> <span>${furniture.quality}</span>
                    </div>
                    <div>
                        <strong>City:</strong> <span>${furniture.city}</span>
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
    });
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
    console.log(tempFurn) 
}

function displayOrderForm()
{
    let html =
    ` <div class="product-wrapper">
        <h1>Product Details</h1>
        <div>
            <strong>Type:</strong> <span id="type">${tempFurn.type}</span>
        </div>
        <div>
            <strong>Condition:</strong> <span id="quality">${tempFurn.quality}</span>
        </div>
        <div>
            <strong>City:</strong> <span id="city">${tempFurn.city}</span>
        </div>
        <div>
            <strong>Price:</strong> <span id="price">$${tempFurn.price}</span>
        </div>
        <div>
            <strong></strong> <img class="resize-image" id="image" src=${tempFurn.image} alt="Product Image">
        </div>
    </div>
    `
    document.getElementById('order').innerHTML = html
}

function postOrder() {
    let buyerid = findBuyerId();
    console.log(buyerid);
    try {
        fetch(orderUrl, {
            method: "POST",
            headers: {
                "Accept": 'application/json',
                "Content-Type": 'application/json'
            },
            body: JSON.stringify({
                furnitureid: tempFurn.id,
                buyerid: buyerid,
                pickupdate: document.getElementById('datePicker').value,
                price: tempFurn.price,
                fname: document.getElementById('firstName').value,
                lname: document.getElementById('lastName').value,
                location: document.getElementById('pickupLocation').value,
                phone: document.getElementById('phoneNumber').value
            })
        })
        .then(() => {
            putSoldFurniture();
            console.log("Account Post success");
            alert("Order submitted successfully!");
            window.location.href = "../resources/buy.html";
        });
    } catch (error) {
        console.error('Error:', error);
    }
}

function putSoldFurniture()
{
    const furnTempUrl = furnitureUrl+"/"+tempFurn.id
    fetch(furnTempUrl,
    {
        method: "PUT",
        headers:
        {
            "Content-Type": 'application/json'
        },
        body: JSON.stringify({
            id : tempFurn.id,
            type: tempFurn.type,
            quality: tempFurn.quality,
            city: tempFurn.city,
            sold: true,
            price: tempFurn.price,
            image: tempFurn.image,
            sellerid: tempFurn.sellerid
        })

    })
}

function findBuyerId() 
{
    let storedEmail = localStorage.getItem('email');
    let foundAccount = myAccounts.find(account => account.email === storedEmail);
    return foundAccount ? foundAccount.id : null;
}

function postFurniture() {
    let buyerid = findBuyerId();
    console.log(buyerid);
    try {
        fetch(furnitureUrl, {
            method: "POST",
            headers: {
                "Accept": 'application/json',
                "Content-Type": 'application/json'
            },
            body: JSON.stringify({
                type: document.getElementById('furnitureType').value,
                quality: document.getElementById('quality').value,
                city: document.getElementById('pickupLocation').value,
                sold: false,
                price: document.getElementById('price').value,
                image: document.getElementById('imageURL').value,
                sellerid: buyerid
            })
        })
        .then(() => {
            console.log("Furniture Post success");
            alert("Congrats On Posting!");
            window.location.href ="../resources/buy.html"
        });
    } catch (error) {
        console.error('Error:', error);
    }
}

async function handleAccountLoad() {
    await getAccountData();
    await getAccount();
    await getFurnitureData();
    displayAccountFurniture();
}

async function handlePageLoad() {
    await handleAccountLoad();
}

async function displayAccountFurniture()
{
    let tempAccount = await getAccount()
    console.log(tempAccount)
    if (tempAccount.admin === false)
    {
        let html = '';
        myFurniture.forEach((furniture) => {
        if (tempAccount.id === furniture.sellerId) {  
            html += `
                <div class="product-wrapper">
                    <h1>Your Listed Item</h1>
                    <div>
                        <strong>Type:</strong> <span>${furniture.type}</span>
                    </div>
                    <div>
                        <strong>Condition:</strong> <span>${furniture.quality}</span>
                    </div>
                    <div>
                        <strong>City:</strong> <span>${furniture.city}</span>
                    </div>
                    <div>
                        <strong>Price:</strong> <span>$${furniture.price}</span>
                    </div>
                    `
            if(furniture.sold === true)
            {
                html += `<div>
                <strong>Sold:</strong> <span>Yes</span>
                </div>`
            }
            else
            {
                html += `<div>
                <strong>Sold:</strong> <span>No</span>
                <button onclick="deleteFurniture('${furniture.id}')">🗑️</button>
                </div>`
            }
            html+=
            `
            <div>
                <img class="resize-image" src="${furniture.image}" alt="Product Image">
                </div>
            </div>
            `
        }
    });
    document.getElementById('acct').innerHTML = html;
    }
    else
    {
        let html = '';
        myFurniture.forEach((furniture) => { 
            html += `
                <div class="product-wrapper">
                    <h1>Client's Listed Item</h1>
                    <div>
                        <strong>Type:</strong> <span>${furniture.type}</span>
                    </div>
                    <div>
                        <strong>Condition:</strong> <span>${furniture.quality}</span>
                    </div>
                    <div>
                        <strong>City:</strong> <span>${furniture.city}</span>
                    </div>
                    <div>
                        <strong>Price:</strong> <span>$${furniture.price}</span>
                    </div>
                    <div>
                        <strong>Seller ID:</strong> <span>${furniture.sellerId}</span>
                    </div>
                    `
            if(furniture.sold === true)
            {
                html += `<div>
                <strong>Sold:</strong> <span>Yes</span>
                <button onclick="deleteFurniture('${furniture.id}')">🗑️</button>
                </div>`
            }
            else
            {
                html += `<div>
                <strong>Sold:</strong> <span>No</span>
                <button onclick="deleteFurniture('${furniture.id}')">🗑️</button>
                </div>`
            }
            html+=
            `
            <div>
                <img class="resize-image" src="${furniture.image}" alt="Product Image">
                </div>
            </div>
            `
    });
        document.getElementById('acct').innerHTML = html;
    }
}

async function getAccount() {
    try {
        let buyerid = findBuyerId();
        let tempAccountUrl = accountUrl + "/" + buyerid;
        let response = await fetch(tempAccountUrl);
        let temp = await response.json();
        let tempAccount = {
            id: temp.id,
            username: temp.username,
            email: temp.email,
            password: temp.password,
            admin: temp.admin
        };
        console.log(tempAccount);
        return tempAccount;
    } catch (error) {
        console.error('Error:', error);
        return null;
    }
}

async function showAccountSettings() {
    await getAccountData();
    await getAccount();
    const account = myAccounts.find(acc => acc.email === localStorage.getItem('email'));
    let html = '<div class="account-settings-box">';
    html += '<h3 class="account-settings-title">Account Settings</h3>';
    html += `<div class="account-settings-info"><strong>Email:</strong> ${account.email}</div>`;
    html += `<div class="account-settings-info"><strong>Password:</strong> <input type="password" id="password" value="${account.password}" disabled></div>`;
    html += '<button class="account-settings-button" onclick="togglePasswordVisibility()">Show Password</button>';
    html += '</div>';
    document.getElementById('acct').innerHTML = html;
}


function togglePasswordVisibility() {
    const passwordInput = document.getElementById('password');
    if (passwordInput.type === 'password') {
        passwordInput.type = 'text';
    } else {
        passwordInput.type = 'password';
    }
}


function deleteFurniture(id) {
    fetch(furnitureUrl + "/" + id, {
        method: "DELETE",
        headers: {
            "Content-Type": 'application/json'
        }
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Failed to delete furniture');
        }
        alert('Furniture successfully deleted!');
        location.reload();
    })
    .catch(error => {
        console.error('Error deleting furniture:', error);
        // Optionally, handle the error, such as displaying an error message to the user
    });
}
