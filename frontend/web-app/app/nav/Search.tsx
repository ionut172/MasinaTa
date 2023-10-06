'use client'
import { useParamsStore } from "@/hooks/useParamsStore";
import { useParams, usePathname, useRouter } from "next/navigation";
import React, { useState } from "react";
import { FaSearch } from "react-icons/fa";
export default function Search() {
    const router = useRouter();
    const pathName = usePathname();

    const setParams = useParamsStore(state=>state.setParams);
    const setSearchValue= useParamsStore(state=>state.setSearchValue)
    
    const [value, setValue]= useState('');

    function OnChange(event: any){
        setValue(event.target.value);

    }
    function search() {
        if(pathName !== '/') router.push('/');
        setParams ({searchTerm:value});
    }
    return (
        <div className=" flex w-[50%] items-center border-2 rounded-full py-2 shadow-sm">
            <input onKeyDown={(e:any)=> {
                if(e.key === 'Enter') search()
            }} value={value}  onChange={OnChange} type="text" placeholder="Search pentru model, culoare, marca masina" className="flex-grow 
            pl-5 bg-transparent 
            focus: outline-none border-transparent 
            focus: border-transparence focus: 
            ring-0 text-sm text-gray-600"></input>
            <button onClick={search}>
                <FaSearch size={34} className='bg-red-400 text-white rounded-full p-2 cursor-pointer mx-2'/>
            </button>
        </div>
    )
}