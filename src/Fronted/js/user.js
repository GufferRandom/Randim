 import {getToken,CheckIfUserIsAuthed} from "./auth.js"
 window.addEventListener("load",async () => {
     const params = new URLSearchParams(window.location.hash.split("#")[1]);
     const code = params.get("code");
     if (code) {
       await getToken(code);
       return;
     }
    if(!CheckIfUserIsAuthed()){
        window.location.href = "index.html";
    }
  });