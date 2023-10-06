'use server'
import { PagedResults } from "@/types";
import { Licitatii } from "@/types";
import { getTokenWorkAround } from "./authActions";
import { fetchWrapper } from "@/lib/fetchWrapper";
import { FieldValue, FieldValues } from "react-hook-form";
import { revalidatePath } from "next/cache";

export async function GetData(query: string) : Promise<PagedResults<Licitatii>>{
    return await fetchWrapper.get(`search${query}`);
    
}
export async function UpdateLicitatiiTest() {
  
    const data = {
        culoare: "red",
        An: 9999,
        Make: "DEADSD",
        Kilometraj: 50000,
        ModelMasina: "TOT EU"
    }
    return await fetchWrapper.put(`licitatii/6a5011a1-fe1f-47df-9a32-b5346b289391`, data);
}
export async function createLicitatii(data: FieldValues){
    return await fetchWrapper.post('licitatii/create', data);
}
export async function getDetailedViewData(id:string) : Promise<Licitatii> {
    return await fetchWrapper.get(`licitatii/${id}`);
}
export async function updateLicitatii(data: FieldValues, id: string) {
    const res =  await fetchWrapper.put(`licitatii/${id}`, data);
    revalidatePath(`/licitatii/${id}`)
    return res;
}
export async function deleteLicitatii(id: string){
    return await fetchWrapper.del(`licitatii/${id}`);
}