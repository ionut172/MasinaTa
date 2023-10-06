'use client'
import React, { useState } from "react";
import Image from "next/image";
type Props = {
    imagineUrl : string
}

export default function LicitatiiImage({imagineUrl}: Props){
    const [isLoading, setLoading] = useState(true);
    return (
        <Image 
             src={imagineUrl}
             alt ='image'
             fill
             priority
             className={`group-hover:opacity-75 duration-700 ease-in-out object-cover ${isLoading ? 'grayscale blur-2xl scale-110' : 'grayscale-0 blur-0 scale-100'}`}
             sizes="(max-width:768px) 10vw, (max-width: 1200px) 50vw, 25vw"
             onLoadingComplete={()=>setLoading(false)}
            />
    )
}