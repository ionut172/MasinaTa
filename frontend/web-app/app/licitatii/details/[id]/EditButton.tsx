'use client'
import { fetchWrapper } from "@/lib/fetchWrapper";
import { Button } from "flowbite-react";
import Link from "next/link";
import React from "react";

type Props = {
    id: string
}

export default function EditButton ({id}: Props){
    return (
        <div>
       <Button outline>
            <Link href={`/licitatii/update/${id}`}>Editeaza licitatia</Link>
       </Button>
  
       
  </div>
    )
}