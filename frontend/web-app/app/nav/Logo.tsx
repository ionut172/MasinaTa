'use client'
import React from "react";
import {AiOutlineCar } from "react-icons/ai";
import Search from "./Search";
import { useParams, usePathname, useRouter } from "next/navigation";
import { useParamsStore } from "@/hooks/useParamsStore";


export default function Logo() {

    const reset = useParamsStore(state=>state.reset);
    const router = useRouter();
    const pathName = usePathname();
    function doReset(){
        if(pathName !== '/') router.push('/') 
        reset();
        
    }
    return (
        
                <div onClick={doReset} className="cursor-pointer flex items-center gap-2 text-3xl font-semibold text-red-500">
                <AiOutlineCar size={34}/>
                    Licitatii
                </div>
           
    )
}