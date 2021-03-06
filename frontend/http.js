const baseUrl = "https://localhost:PORTA_API/api/v1";

function authHeaders() {
  return { Authorization: `Bearer ${sessionStorage.getItem("tokenAcesso")}` };
}

function isLogado() {
  return !!sessionStorage.getItem("tokenAcesso");
}

function login(email, senha) {
  return fetchJson(`${baseUrl}/Auth/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ email, senha }),
  }).then(
    (data) => {
      sessionStorage.setItem("tokenAcesso", data.tokenAcesso);
    },
    () => {
      sessionStorage.removeItem("tokenAcesso");
      throw new Error("Falha no login. Verifique as credĂȘnciais e tente novamente.");
    }
  );
}

function fetchVoid(url, options) {
  return fetch(url, options).then((r) => {
    if (r.ok) {
      return;
    } else {
      throw new Error(r.statusText);
    }
  });
}

function fetchJson(url, options) {
  return fetch(url, options)
    .then((r) => {
      if (r.ok) {
        return r.json();
      } else {
        throw new Error(r.statusText);
      }
    })
    .then((json) => json.data);
}

function listaLivros() {
  return fetchJson(`${baseUrl}/Livros`, { headers: authHeaders() });
}

function listaAutores() {
  return fetchJson(`${baseUrl}/Autores`, { headers: authHeaders() });
}

function criaAutor(nome, sobrenome) {
  fetchVoid(`${baseUrl}/Autores`, {
    method: "POST",
    headers: { ...authHeaders(), "Content-Type": "application/json" },
    body: JSON.stringify({ nome, sobrenome }),
  });
}
