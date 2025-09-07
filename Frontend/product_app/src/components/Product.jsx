const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || "https://localhost:7212/api";
import React, { useState, useEffect } from 'react'
import axios from 'axios'

function Product() {
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState(null)
  const [success, setSuccess] = useState(null)
  const [form, setForm] = useState({ name: '', price: '' })
  const [editingId, setEditingId] = useState(null)
  const [products, setProducts] = useState([])

  const fetchProducts = async () => {
    setLoading(true)
    setError(null)
    try {
      const response = await axios.get(`${API_BASE_URL}/ProductAPI`, {
        headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json'
        }
      })
      setProducts(response.data)
    } catch (error) {
      setError('Error fetching products')
      console.error('Error fetching products:', error)
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    fetchProducts()
  }, [])

  const handleInputChange = (e) => {
    const { name, value } = e.target
    setForm(prev => ({ ...prev, [name]: value }))
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    if (!form.name || !form.price) {
      setError('Please fill in all fields')
      return
    }
    setLoading(true)
    setError(null)
    setSuccess(null)
    try {
     
      // Create product
await axios.post(`${API_BASE_URL}/ProductAPI`, {
  pName: form.name,   
  price: form.price
}, {
  headers: { 'Content-Type': 'application/json' }
})
        setSuccess('Product added successfully')
      setForm({ name: '', price: '' })
      fetchProducts()
      setTimeout(() => setSuccess(null), 3000)
    } catch (error) {
      setError('Error saving product')
      console.error('Error saving product:', error)
    } finally {
      setLoading(false)
    }
  }

  const handleEdit = (product) => {
    setForm({ PName: product.name, Price: product.price })
    setEditingId(product.id)
    setError(null)
  }

  const handleDelete = async (id) => {
    setLoading(true)
    setError(null)
    setSuccess(null)
    try {
      await axios.delete(`${API_BASE_URL}/ProductAPI/${id}`)
      setSuccess('Product deleted successfully')
      fetchProducts()
      setTimeout(() => setSuccess(null), 3000)
    } catch (error) {
      setError('Error deleting product')
      console.error('Error deleting product:', error)
    } finally {
      setLoading(false)
    }
  }

  return (
    <>
      <h2>All products</h2>
      {loading && <p className="loading">Loading...</p>}
      {error && <p className="error">{error}</p>}
      {success && <p className="success">{success}</p>}

      <form className="product-form" onSubmit={handleSubmit}>
        <input
          type="text"
          name="name"
          placeholder="Product name"
          value={form.name}
          onChange={handleInputChange}
        />
        <input
          type="number"
          name="price"
          placeholder="Product price"
          value={form.price}
          onChange={handleInputChange}
        />
        <button type="submit">{editingId ? 'Update' : 'Add'} Product</button>
        {editingId && (
          <button
            type="button"
            onClick={() => {
              setEditingId(null)
              setForm({ name: '', price: '' })
              setError(null)
            }}
          >
            Cancel
          </button>
        )}
      </form>

      <ul className="product-list">
        {products.map((product) => (
          <li key={product.id} className="product-item">
            <span>{product.name} - ${product.price}</span>
            <div>
              <button onClick={() => handleEdit(product)}>Edit</button>
              <button onClick={() => handleDelete(product.id)}>Delete</button>
            </div>
          </li>
        ))}
      </ul>
    </>
  )
}

export default Product
