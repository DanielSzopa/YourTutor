const filter = () => {
  let priceFrom = getPriceFrom();
  let priceTo = getPriceTo();
  let search = document.querySelector('#searchFilter').value;
  let remotely = document.querySelector('#remotelyFilter').checked;

  let query = '/Offer?';
  query = query.concat(
    '&priceFrom=',
    priceFrom,
    '&priceTo=',
    priceTo,
    '&searchString=',
    search,
    '&isRemotely=',
    remotely
  );

  let filterBtn = document.querySelector('#filerBtn');
  filterBtn.setAttribute('href', query);
};

const getPriceFrom = () => {
  let inputElement = document.querySelector('#priceFromFilter');
  if (inputElement.value === '') return 0;

  return inputElement.value;
};

const getPriceTo = () => {
  let inputElement = document.querySelector('#priceToFilter');
  if (inputElement.value === '') return 0;

  return inputElement.value;
};
