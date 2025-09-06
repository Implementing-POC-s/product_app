import React, { useState,useEffect } from 'react'
import axios from 'axios'
function Product() {
    const [products, setProducts] = useState([])
    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const response = await axios.get("https://localhost:7212/api/ProductAPI", {
                    headers: {
                        'Content-Type': 'application/json',
                        'Accept': 'application/json'
                    }
                });
                const data = response.data;
                console.log(response);
                setProducts(data);
                console.log(data);
            } catch (error) {
                console.error('Error fetching products:', error);
            }
        };
        fetchProducts();
      }, [])//dependency array
    return (
      
      <>
          <div>
              All products
          </div>
          <div>
              {products.map((product) => (
                  <div id={product.id}>
                      <span>{product.name}</span>-----<span>{product.price}</span>
                  </div>
              ))}
              </div>

              
          
              
    </>
  )
}

export default Product
