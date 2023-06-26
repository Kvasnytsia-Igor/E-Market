async function getLaptopsWithPagination(url = "") {
  const response = await fetch(url);
  return response.json();
}

getLaptopsWithPagination("http://localhost:5189/api/laptops/1/22").then(
  (data) => {
    console.log(data.paginatedList.items[0]);
  }
);
