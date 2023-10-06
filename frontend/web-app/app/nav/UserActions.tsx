'use client'
import { Button } from "flowbite-react";
import { Dropdown } from 'flowbite-react';
import { User } from "next-auth";
import {HiCog, HiUser} from "react-icons/hi";

import Link from "next/link";
import React, { useState } from "react";
import { AiFillCar, AiFillTrophy, AiOutlineLogout } from "react-icons/ai";
import { signOut } from "next-auth/react";
import { usePathname, useRouter } from "next/navigation";
import { useParamsStore } from "@/hooks/useParamsStore";

type Props = {
    user: User
}
export default function UserAction({user}:Props) {

    const router = useRouter();
    const pathName= usePathname();
    const setParams = useParamsStore(state=>state.setParams);
    
    function setWinner() {
      setParams({castigator: user.username, vanzator: undefined})
      if(pathName !== "/") router.push('/');
    }
    function setSeller() {
      setParams({vanzator: user.username, castigator: undefined})
      if(pathName !== "/") router.push('/');
    }

    return (
      
            <Dropdown
              inline
              label={'Bine ai venit '+ user.name}
            >
              <Dropdown.Item icon={HiUser} onClick={setSeller}>
                <Link href="/">Licitatiile mele</Link>
              </Dropdown.Item>
              <Dropdown.Item icon={AiFillTrophy} onClick={setWinner}>
                <Link href="/">Licitatii castigate</Link>
              </Dropdown.Item>
              <Dropdown.Item icon={HiCog}>
                <Link href="/licitatii/create">Vand Masina</Link>
              </Dropdown.Item>
              <Dropdown.Item icon={AiFillCar}>
                <Link href="/session">Sesiune (dev only)</Link>
              </Dropdown.Item>
              <Dropdown.Divider/>
              <Dropdown.Item icon={AiOutlineLogout} onClick={() => signOut({callbackUrl: "/"})} color="black">
                <Link href="/">Logout</Link>
              </Dropdown.Item>
            </Dropdown>
          )
        }
        
        
        
        
        
  