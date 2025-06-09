const baseUrl = "http://localhost:8080";
const realm = "RandimSocialMedia";
const client_id = "public-client";
const redirect_uri = "http://localhost:5500/main.html";

const generateRandomState=()=> {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
        var r = Math.random() * 16 | 0,
            v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
  }
 const redirectToKeycloak = () => {
    const state = generateRandomState();
    const nonce = generateRandomState();
    const urlParams = {
        client_id,
        redirect_uri,
        response_mode: "fragment",
        response_type: "code",
        scope: "openid",
        nonce,
        state,
    };
    const conectionURI = new URL(`${baseUrl}/realms/${realm}/protocol/openid-connect/auth`);
    for(const key of Object.keys(urlParams)){
        conectionURI.searchParams.append(key, urlParams[key]);
    }
    window.location.href = conectionURI;
}
export const getToken = async (code) => {
    const tokenUri = new URL(`${baseUrl}/realms/${realm}/protocol/openid-connect/token`);
    const body = new URLSearchParams();
    body.append("client_id", client_id);
    body.append("redirect_uri", redirect_uri);
    body.append("grant_type", "authorization_code");
    body.append("code", code);
    const response = await fetch(tokenUri, {
      method: 'POST',
      headers: {'Content-Type': 'application/x-www-form-urlencoded'},
      body,
    });
    if (!response.ok) {
      console.error('Failed to fetch token:', response.status, await response.text());
      return;
    }
    const jsonData = await response.json();
    localStorage.setItem("token", jsonData.access_token);
    localStorage.setItem("id_token", jsonData.id_token);
    localStorage.setItem("refresh_token", jsonData.refresh_token);
    localStorage.setItem("session_state", jsonData.session_state);
    history.replaceState({}, '', '/main.html');
    location.reload();
  }
  
const logout = () => {
    const logoutURI = new URL(`${baseUrl}/realms/${realm}/protocol/openid-connect/logout`);
    const id_token_hint = localStorage.getItem("id_token");
    const urlParams = {
        client_id,
        post_logout_redirect_uri: redirect_uri,
        id_token_hint,
    };
    for(const key of Object.keys(urlParams)){
        logoutURI.searchParams.append(key, urlParams[key]);
    }
    localStorage.clear();
    window.location.href = logoutURI;
}
export function SignIn(){
    redirectToKeycloak();
}
export function RedirectToMainPage(){
    return; 
}
 window.addEventListener("load",async () => {
    const params = new URLSearchParams(window.location.hash.split("#")[1]);
    const code = params.get("code");
    if (code) {
      await getToken(code);
      return;
    }
    UserRedirectBasedOnAuth();
  });
export function UserRedirectBasedOnAuth(){
    const token = localStorage.getItem("token");
    if (token !== null) {
        RedirectToMainPage();
    } else {
        SignIn();
    }
}
export function CheckIfUserIsAuthed(){
    const token = localStorage.getItem("token");
    if (token !== null) {
        return true;
    } else {
        return false;
    }
}
window.SignIn = SignIn;