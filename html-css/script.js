async function getLaptopsWithPagination(url = "") {
  const response = await fetch(url);
  return response.json();
}

const itemsCount = 10;
const startPage = 1;

getLaptopsWithPagination(
  `http://localhost:5189/api/laptops/${startPage}/${itemsCount}`
).then(processReceivedData);



function processReceivedData(data) {
  const rows = document.querySelectorAll('.table-row');
  console.log(rows);
  rows.forEach((row) => {
    row.remove();
  });

  //////
  let entities = data.paginatedList.items;
  for (let i = 0; i < entities.length; i++) {
    document.querySelector(
      "#content"
    ).innerHTML += `<li class="table-row">
    <div class="col col-1" data-label="Brand:">${entities[i].brand}</div>
    <div class="col col-2" data-label="Series:">${entities[i].series}</div>
    <div class="col col-3" data-label="Price:">${entities[i].price}</div>
    <div class="col col-4" data-label="Created:">${entities[i].created}</div>
    <div class="col col-5" data-label="Modified:">${entities[i].lastModifiedBy}</div>
    </li>`;
  }

//   ///////
  const pagerItems = document.querySelectorAll('.pager__item');
  console.log(pagerItems);
  pagerItems.forEach((item) => {
    item.remove();
  });
  console.log(pagerItems);
  const totalPages = data.paginatedList.totalPages;
  for (let i = 0; i < totalPages; i++) {
    document.querySelector(
      "#pager"
    ).innerHTML += `<li class="pager__item">
              <a class="pager__link">${i + 1}</a>
              </li>`;
  }
  const pageNumber = data.paginatedList.pageNumber;
  document
    .querySelector("#pager")
    .children[pageNumber - 1].classList.add("active");

//////
  const buttons = document.querySelectorAll(".pager__link");
  buttons.forEach((button) => {
    button.addEventListener("click", function (event) {
      let pageNumber = event.target.textContent;
      getLaptopsWithPagination(
        `http://localhost:5189/api/laptops/${pageNumber}/${itemsCount}`
      ).then(processReceivedData);
    });
  });
}
