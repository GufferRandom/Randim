export async function FetchGetDefaultAsync(url){
    const token = localStorage.getItem("token");
    let resp = await fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },
        mode: "cors"
    });
    ResponseDefaultData(resp);

}
async function ResponseDefaultData(resp) {
    try {
        if (!resp.ok) {
            const errorDetails = await resp.text();
            throw new Error(`Could not fetch. Status: ${resp.status}, Body: ${errorDetails}`);
        }
        return await resp.json();
    } catch (error) {
        console.error("FetchDefaultData error:", error);
        throw error; 
    }
}
