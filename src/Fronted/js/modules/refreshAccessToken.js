import {jwtDecode} from "../lib/jwt-decode-esm.js";
export async function RefreshAccessToken(){
    const token = localStorage.getItem("token");
    if(!token){
        return;
    }
    const decodedToken = jwtDecode(token);
    const expDate = new Date(decodedToken.exp * 1000); 
    if(expDate>new Date()){
        return;
    }
    const params = new URLSearchParams();
    const refreshToken = localStorage.getItem("refresh_token");
    params.append("grant_type", "refresh_token");
    params.append("client_id", "public-client");
    params.append("refresh_token", refreshToken);
    try {
        const response = await fetch("http://localhost:8080/realms/RandimSocialMedia/protocol/openid-connect/token", {
          method: "POST",
          headers: {
            "Content-Type": "application/x-www-form-urlencoded",
          },
          body: params.toString(),
        });
        if (!response.ok) {
          const errorText = await response.text();
          throw new Error(`Failed to refresh token: ${response.status} - ${errorText}`);
        }
        const data = await response.json();
        localStorage.setItem("token", data.access_token);
        localStorage.setItem("refresh_token", data.refresh_token);
        return data.access_token;
      } catch (error) {
        return null;
      }
}